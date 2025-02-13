using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpiritMod.Buffs;
using SpiritMod.Items.Material;
using SpiritMod.Items.Weapon.Magic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.NPCs.BlueMoon.GlowToad
{
	public class GlowToad : ModNPC
	{
		int timer = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glow Toad");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 64;
			npc.height = 60;
			npc.damage = 49;
			npc.defense = 14;
			npc.lifeMax = 380;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath5;
			npc.value = 2000f;
			npc.knockBackResist = 0.5f;
            banner = npc.type;
            bannerItem = ModContent.ItemType<Items.Banners.GlowToadBanner>();
            // npc.aiStyle = 26;
            // aiType = NPCID.Unicorn;
        }
		public override void HitEffect(int hitDirection, double damage)
		{
            Main.projectile[tongueproj].active = false;
			Main.PlaySound(SoundID.Frog, (int)npc.position.X, (int)npc.position.Y);

			for (int k = 0; k < 5; k++)
				Dust.NewDust(npc.position, npc.width, npc.height, 180, hitDirection, -1f, 0, default(Color), 1f);
			if (npc.life <= 0) {
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GlowToad/GlowToad1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GlowToad/GlowToad2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GlowToad/GlowToad3"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GlowToad/GlowToad4"), 1f);
			}

		}
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(tongueOut);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			tongueOut = reader.ReadBoolean();
		}
		bool tongueOut = false;
		
		public override void AI()
		{
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			if (Math.Abs(player.position.X - npc.position.X) < 190 && npc.collideY) {
				tongueOut = true;
			}
			if (!tongueOut && npc.ai[2] > 4) {
				npc.ai[0]++;
			}
			if (!npc.collideY) {
				npc.velocity.X *= 1.045f;
			}
			npc.ai[2]++;
			if (npc.ai[0] % 90 == 0 && !tongueOut) {

				if (npc.ai[1] == 0) {
					npc.ai[1] = 1;
					npc.ai[2] = 0;
				}
				npc.ai[2]++;
				npc.velocity.Y = -7;
				if (player.position.X > npc.position.X) {
					npc.velocity.X = 10;
				}
				else {
					npc.velocity.X = -10;
				}
			}
			else {
				npc.ai[1] = 0;
			}
			if (npc.ai[3] == 0) {
				if (player.position.X > npc.position.X) {
					npc.spriteDirection = 0;
				}
				else {
					npc.spriteDirection = 1;
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return MyWorld.BlueMoon && spawnInfo.player.ZoneOverworldHeight ? 7f : 0f;
		}
		float frameCounter = 0;
		int tongueproj = 0;
		public override void FindFrame(int frameHeight)
		{
			Player player = Main.player[npc.target];
			if (!npc.collideY) {
				//npc.frameCounter += 0.40f;
				// npc.frameCounter %= 5;
				// int frame = (int)npc.frameCounter;
				npc.frame.Y = 0;
				frameCounter = 0;
			}
			else if (!tongueOut) {
				npc.frame.Y = frameHeight;
				frameCounter = 0f;
			}
			if (tongueOut) {
				// timer = 100;
				if (npc.ai[3] == 0) {
					frameCounter += 0.11f;
				}
				npc.frame.Y = ((int)(frameCounter % 5) + 1) * frameHeight;
				if (npc.frame.Y / frameHeight == 5 && npc.ai[3] == 0) {
					npc.TargetClosest(true);
					npc.ai[3] = 1;
					npc.knockBackResist = 0f;
					if (Main.netMode != NetmodeID.MultiplayerClient) {
						if (npc.spriteDirection == 0) {
							Main.PlaySound(SoundID.Frog, (int)npc.position.X, (int)npc.position.Y);
							tongueproj = Projectile.NewProjectile(npc.Center + new Vector2(21, 8), new Vector2(11, 0), ModContent.ProjectileType<GlowTongue>(), (int)(npc.damage / 1.7f), 1, Main.myPlayer, 1);
						}
						else {
							Main.PlaySound(SoundID.Frog, (int)npc.position.X, (int)npc.position.Y);
							tongueproj = Projectile.NewProjectile(npc.Center + new Vector2(-18, 8), new Vector2(-11, 0), ModContent.ProjectileType<GlowTongue>(), (int)(npc.damage / 1.7f), 1, Main.myPlayer, 0);
						}
					}
					npc.netUpdate = true;
				}
				if (npc.ai[3] == 1) {
					Projectile tongue = Main.projectile[tongueproj];
					if (!tongue.active) {
						npc.ai[3] = 0;
						tongueOut = false;
						npc.ai[0] = 80;
						npc.knockBackResist = 0.5f;
					}
					else {
						if (tongue.ai[0] == 0) {
							npc.direction = -1;
							npc.spriteDirection = -1;
						}
						else {
							npc.direction = 1;
							npc.spriteDirection = 1;
						}
						tongue.netUpdate = true;
					}
				}
			}
		}
		//int tongueDirection = 0;
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			var effects = npc.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
							 drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
			return false;
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			GlowmaskUtils.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/BlueMoon/GlowToad/GlowToad_Mask"));
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(5) == 0)
				target.AddBuff(ModContent.BuffType<StarFlame>(), 200);
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextBool(5))
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MoonStone>());
			if (Main.rand.NextBool(100))
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<StopWatch>());
		}

	}
}

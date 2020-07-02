﻿using SpiritMod.Projectiles.Hostile;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpiritMod.Tide;

namespace SpiritMod.NPCs.Tides
{
	public class KakamoraShielderRare : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kakamora Shielder");
			Main.npcFrameCount[npc.type] = 5;
		}

		public override void SetDefaults()
		{
			npc.width = 48;
			npc.height = 52;
			npc.damage = 24;
			npc.defense = 4;
			aiType = NPCID.SnowFlinx;
			npc.aiStyle = 3;
			npc.lifeMax = 120;
			npc.knockBackResist = .20f;
			npc.value = 200f;
			npc.noTileCollide = false;
			npc.HitSound = SoundID.NPCHit2;
			npc.DeathSound = SoundID.NPCDeath1;
		}
		bool blocking = false;
		int blockTimer = 0;
		public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            var list2 = Main.projectile.Where(x => x.Hitbox.Intersects(npc.Hitbox));
            foreach (var proj in list2)
            {
                if (proj.type == ModContent.ProjectileType<ShamanBolt>() && proj.active && npc.life < npc.lifeMax - 30)
                {
                    npc.life += 30;
                    npc.HealEffect(30, true);
                    proj.active = false;
                }
                else if (proj.type == ModContent.ProjectileType<ShamanBolt>() && proj.active && npc.life > npc.lifeMax - 30)
                {
                    npc.life += npc.lifeMax - npc.life;
                    npc.HealEffect(npc.lifeMax - npc.life, true);
                    proj.active = false;
                }
            }
            if (blockTimer == 200) {
				Main.PlaySound(SoundLoader.customSoundType, npc.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Kakamora/KakamoraHit"));
			}
			if(blockTimer > 200) {
				blocking = true;
			}
			if(blockTimer > 350) {
				blocking = false;
				blockTimer = 0;
			}
			if(blocking) {
				npc.aiStyle = 0;
				npc.velocity.X = 0;
                npc.noGravity = false;
                npc.defense = 999;
				npc.HitSound = SoundID.NPCHit4;
				if(player.position.X > npc.position.X) {
					npc.spriteDirection = 1;
				} else {
					npc.spriteDirection = -1;
				}
			} else {
                if (npc.wet)
                {
                    npc.noGravity = true;
                    npc.velocity.Y -= .085f;
                }
                else
                {
                    npc.noGravity = false;
                }
                npc.spriteDirection = npc.direction;
				npc.aiStyle = 3;
				npc.defense = 6;
				npc.HitSound = SoundID.NPCHit2;
				var list = Main.npc.Where(x => x.Hitbox.Intersects(npc.Hitbox));
				foreach(var npc2 in list) {
					if(npc2.type == ModContent.NPCType<LargeCrustecean>() && npc.Center.Y > npc2.Center.Y && npc2.active) {
						npc.velocity.X = npc2.direction * 7;
						npc.velocity.Y = -2;
						Main.PlaySound(SoundLoader.customSoundType, npc.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Kakamora/KakamoraHit"));
					}
				}
			}
		}

		public override void FindFrame(int frameHeight)
		{
			if(npc.collideY && !blocking) {
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 4;
				int frame = (int)npc.frameCounter;
				npc.frame.Y = frame * frameHeight;
			}
			if(blocking) {
				npc.frame.Y = 4 * frameHeight;
			}
		}
		public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
		{
			if(blocking) {
				projectile.hostile = true;
				projectile.friendly = false;
				Main.PlaySound(3, npc.position, 4);
				projectile.penetrate = 2;
				projectile.velocity.X = projectile.velocity.X * -1f;
			}
		}
		public override void HitEffect(int hitDirection, double damage)
        {
            int d = 207;
            int d1 = 207;
            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
                Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, default(Color), .34f);
            }
            if (npc.life <= 0) {
				Main.PlaySound(SoundLoader.customSoundType, npc.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Kakamora/KakamoraDeath"));
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Kakamora_Gore1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Kakamora_Gore2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Kakamora_Gore3"), 1f);
                if (TideWorld.TheTide)
                {
                    TideWorld.TidePoints += 1;
                }
            } else if(!blocking) {
				Main.PlaySound(SoundLoader.customSoundType, npc.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Kakamora/KakamoraHit"));
			}
		}
	}
}
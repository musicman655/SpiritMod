
using Microsoft.Xna.Framework;
using SpiritMod.Items.Placeable.Tiles;
using SpiritMod.NPCs;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SpiritMod.Tiles.Ambient.IceSculpture.Hostile
{
	public class IceWheezerHostile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
			16,
			16,
			16
			};
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Frozen Wheezer");
			dustType = 51;
			AddMapEntry(new Color(200, 200, 200), name);
		}
		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 2;
		}
		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			if (!NPC.downedBoss3) {
				return false;
			}
			return true;
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 27));
				Item.NewItem(i * 16, j * 16, 48, 48, ModContent.ItemType<CreepingIce>(), Main.rand.Next(6, 13));
			}
		}
		public override void NearbyEffects(int i, int j, bool closer)
		{

			Player player = Main.LocalPlayer;
			if (closer && NPC.downedBoss3) {
				int distance1 = (int)Vector2.Distance(new Vector2(i * 16, j * 16), player.Center);
				if (distance1 < 56) {
					Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 27));
					int n = NPC.NewNPC((int)i * 16, (int)j * 16, ModContent.NPCType<NPCs.Wheezer.Wheezer>(), 0, 2, 1, 0, 0, Main.myPlayer);
					Main.npc[n].GivenName = "Icy Wheezer";
					Main.npc[n].lifeMax = Main.npc[n].lifeMax * 2;
					Main.npc[n].life = Main.npc[n].lifeMax;
					Main.npc[n].damage = (int)(Main.npc[n].damage * 1.5f);
					Main.npc[n].knockBackResist = 0.4f;
                    Main.npc[n].netUpdate = true;
                    WorldGen.KillTile(i, j);
				}
			}
		}
	}
}
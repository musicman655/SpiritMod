using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Walls.Natural
{
	public class ReachWallNatural : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[Type] = true;
			drop = 747;
			AddMapEntry(new Color(58, 60, 60));
		}
	}
}
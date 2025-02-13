﻿using Terraria;
using System;
using SpiritMod.Tiles.Block;
using Terraria.ID;
using Terraria.ModLoader;
// If you are using c# 6, you can use: "using static Terraria.Localization.GameCulture;" which would mean you could just write "DisplayName.AddTranslation(German, "");"

namespace SpiritMod.Items.Placeable.Tiles
{
	public class AsteroidBlock : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Ore-rich space debris'\nCan be used with an extractinator");
            ItemID.Sets.ExtractinatorMode[item.type] = item.type;
        }

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = ModContent.TileType<Asteroid>();
		}
		
		public override void ExtractinatorUse(ref int resultType, ref int resultStack)
        {
            resultType = Main.rand.Next(new int[] { 11, 12, 13, 14, 699, 700, 701, 702, 999, 182, 178, 179, 177, 180, 181 });
            resultStack = Main.rand.Next(2, 4);
        }
    }
}

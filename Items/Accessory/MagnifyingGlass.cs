using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Accessory
{
    public class MagnifyingGlass : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magnifying Glass");
			Tooltip.SetDefault("Increases critical strike chance by 4%\nRight click to zoom out slightly when not holding a weapon");
		}


        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.buyPrice(0, 0, 20, 0);
            item.rare = 1;

            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>(mod).magnifyingGlass = true;
            player.magicCrit += 4;
            player.meleeCrit += 4;
            player.thrownCrit += 4;
            player.rangedCrit += 4;
        }
    }
}

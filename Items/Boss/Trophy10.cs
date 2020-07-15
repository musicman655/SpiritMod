using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Boss
{
	public class Trophy10 : ModItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("R'lyehian Trophy");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 0;
			item.rare = ItemRarityID.Blue;
			item.createTile = mod.TileType("Trophy10Tile");
			item.placeStyle = 0;
		}
	}
}
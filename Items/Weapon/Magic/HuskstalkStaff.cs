using SpiritMod.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Weapon.Magic
{
	public class HuskstalkStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Huskstalk Staff");
			Tooltip.SetDefault("Shoots consecutive leaves");
		}


		public override void SetDefaults()
		{
			item.damage = 11;
			item.magic = true;
			item.mana = 10;
			item.width = 40;
			item.height = 40;
			item.useTime = 10;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			Item.staff[item.type] = true;
			item.noMelee = true;
			item.knockBack = 6;
			item.useTurn = false;
			item.value = Terraria.Item.sellPrice(0, 0, 15, 0);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;
			item.shoot = ProjectileID.Leaf;
			item.shootSpeed = 5.5f;
			item.reuseDelay = 45;
		}

	}
}

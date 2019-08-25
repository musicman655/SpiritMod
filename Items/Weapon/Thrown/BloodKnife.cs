using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpiritMod.Items.Weapon.Thrown
{
	public class BloodKnife : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blood Dagger");
			Tooltip.SetDefault("Inflicts Blood Corruption");
		}


        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.width = 24;
            item.height = 46;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.thrown = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("BloodKnife");
            item.useAnimation = 20;
            item.consumable = true;
            item.maxStack = 999;
            item.useTime = 20;
            item.shootSpeed = 12f;
            item.damage = 17;
            item.knockBack = 2.7f;
			item.value = Item.sellPrice(0, 0, 0, 30);
            item.rare = 2;
            item.autoReuse = true;
            item.maxStack = 999;
            item.consumable = true;
        }
    	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
		     Lighting.AddLight(item.position, 0.92f, .14f, .24f);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BloodFire", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 40);
            recipe.AddRecipe();
        }
    }
}

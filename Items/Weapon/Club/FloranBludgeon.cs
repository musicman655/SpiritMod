﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SpiritMod.Items.Weapon.Club
{
    public class FloranBludgeon : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Floran Bludgeon");
            Tooltip.SetDefault("Releases a shockwave along the ground");
        }

        public override void SetDefaults()
        {
            item.channel = true;
            item.damage = 16;
            item.width = 58;
            item.height = 58;
            item.useTime = 320;
            item.useAnimation = 320;
            item.crit = 4;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.melee = true;
            item.noMelee = true;
            item.knockBack = 8;
            item.useTurn = false;
            item.value = Terraria.Item.sellPrice(0, 0, 22, 0);
            item.rare = 1;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("FloranBludgeonProj");
            item.shootSpeed = 6f;
            item.noUseGraphic = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Items.Material.FloranBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<Items.Material.EnchantedLeaf>(), 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
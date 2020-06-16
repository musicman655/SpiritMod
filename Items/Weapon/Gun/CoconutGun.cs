using Microsoft.Xna.Framework;
using SpiritMod.Items.Material;
using SpiritMod.Projectiles.Bullet;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Items.Weapon.Gun
{
    public class CoconutGun : ModItem

    {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Coconut Gun");
            Tooltip.SetDefault("'Fires in Spurts' \n'If it shoots ya, it's gonna hurt!'");
        }

        public override void SetDefaults() {
            item.damage = 27;
            item.ranged = true;
            item.width = 65;
            item.height = 21;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 9;
            item.useTurn = true;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 8;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<CoconutSpurt>();
            item.shootSpeed = 1f;
            item.crit = 14;
            item.UseSound = SoundID.Item36;
           // item.useAmmo = AmmoID.Bullet;
        }

         public override Vector2? HoldoutOffset() {
            return new Vector2(-7, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) 
        {
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(position.X, position.Y, (speedX * Main.rand.Next(25,30) / 4) + (Main.rand.Next(-1,2) * .66f), (speedY * Main.rand.Next(25,30) / 4) + (Main.rand.Next(-1,2) * .66f), ModContent.ProjectileType<CoconutSpurt>(), damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
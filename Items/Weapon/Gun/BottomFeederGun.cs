using Microsoft.Xna.Framework;
using SpiritMod.Projectiles.Hostile;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace SpiritMod.Items.Weapon.Gun
{
	public class BottomFeederGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Belcher");
			Tooltip.SetDefault("Converts bullets into clumps of rotting flesh");
		}

		public override void SetDefaults()
		{
			item.damage = 7;
			item.ranged = true;
			item.width = 28;
			item.height = 14;
			item.useTime = 11;
			item.useAnimation = 11;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 0f;
			item.useTurn = false;
			item.value = Terraria.Item.sellPrice(0, 1, 32, 0);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.NPCHit18;
			item.autoReuse = true;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 5f;
			item.useAmmo = AmmoID.Bullet;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-6, 0);
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 37f;
			if(Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) {
				position += muzzleOffset;
			}
			int bloodproj;
			bloodproj = Main.rand.Next(new int[] { 
				ModContent.ProjectileType<Feeder1>(), 
				ModContent.ProjectileType<Feeder2>(), 
				ModContent.ProjectileType<Feeder3>() 
			});
			float spread = 30 * 0.0174f;//45 degrees converted to radians
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX, speedY);
			double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
			speedX = baseSpeed * (float)Math.Sin(randomAngle);
			speedY = baseSpeed * (float)Math.Cos(randomAngle);
			int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, bloodproj, damage, knockBack, player.whoAmI, 0f, 0f);
			Main.projectile[p].hostile = false;
			Main.projectile[p].friendly = true;
			Main.projectile[p].ranged = true;
			Main.projectile[p].penetrate = 1;
			return false;
		}
	}
}
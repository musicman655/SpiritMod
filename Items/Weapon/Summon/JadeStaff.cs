using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace SpiritMod.Items.Weapon.Summon
{
	public class JadeStaff : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Staff of the Jade Dragon");
            Tooltip.SetDefault("Summons two revolving ethereal dragons");
		}

		 public override void SetDefaults()
        {
             item.damage = 13;
            item.summon = true;
            item.mana = 60;
            item.width = 44;
            item.height = 48;
            item.useTime = 80;
			item.useAnimation =80;
			item.useStyle = 5;
			Item.staff[item.type] = true;
			item.noMelee = true; 
            item.knockBack = 0;
            item.value = 20000;
            item.rare = 3;
            item.UseSound = SoundID.Item20;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("DragonHeadOne");
            item.shootSpeed = 3f;
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124));
            int dragonLength = 8;
			int offset = 0;
			if (speedX > 0)
			{
				offset = -32;
			}
			else
			{
				offset = 32;
			}
			
			int latestprojectile = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DragonHeadOne"), damage, knockBack, player.whoAmI, speedX, speedY); //bottom
			for (int i = 0; i < dragonLength; ++i)
			{
				latestprojectile = Projectile.NewProjectile(position.X + (i * offset), position.Y, 0, 0, mod.ProjectileType("DragonBodyOne"), damage, 0, player.whoAmI, 0, latestprojectile);
			}
			latestprojectile = Projectile.NewProjectile(position.X + (dragonLength * offset), position.Y, 0, 0, mod.ProjectileType("DragonTailOne"), damage, 0, player.whoAmI, 0, latestprojectile);
			
			latestprojectile = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DragonHeadTwo"), damage, knockBack, player.whoAmI, speedX, speedY); //bottom
			for (int j = 0; j < dragonLength; ++j)
			{
				latestprojectile = Projectile.NewProjectile(position.X + (j * offset), position.Y, 0, 0, mod.ProjectileType("DragonBodyTwo"), damage, 0, player.whoAmI, 0, latestprojectile);
			}
			latestprojectile = Projectile.NewProjectile(position.X + (dragonLength * offset), position.Y, 0, 0, mod.ProjectileType("DragonTailTwo"), damage, 0, player.whoAmI, 0, latestprojectile);
			//Main.projectile[(int)latestprojectile].realLife = projectile.whoAmI;
			return true;
		}
	}
}
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpiritMod.Projectiles
{
	public class WitherOrb : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wither Orb");

		}

		public override void SetDefaults()
		{
			projectile.hostile = false;
			projectile.alpha = 255;
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.timeLeft = 360;
		}

		public override bool PreAI()
		{
			int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 60, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
			int dust1 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 5, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust1].noGravity = true;
			Main.dust[dust].velocity *= 0f;
			Main.dust[dust1].velocity *= 0f;
			Main.dust[dust1].scale = 1.2f;
			Main.dust[dust].scale = 1.2f;

			return true;
		}

		int timer = 20;
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			projectile.Center = new Vector2(player.Center.X + (player.direction > 0 ? 40 : -40), player.position.Y - 40);   // I dont know why I had to set it to -60 so that it would look right   (change to -40 to 40 so that it's on the floor)
			projectile.rotation += player.direction * 0.5f;
			var list = Main.projectile.Where(x => x.Hitbox.Intersects(projectile.Hitbox));
			foreach (var proj in list)
			{
				projectile.localAI[0] += 1f;
				if (projectile.localAI[0] >= 10f)
				{
					projectile.localAI[0] = 0f;
					int num416 = 0;
					int num417 = 0;
					float num418 = 0f;
					int num419 = projectile.type;
					for (int num420 = 0; num420 < 1000; num420++)
					{
						if (Main.projectile[num420].active && Main.projectile[num420].owner == projectile.owner && Main.projectile[num420].type == num419 && Main.projectile[num420].ai[1] < 3600f)
						{
							num416++;
							if (Main.projectile[num420].ai[1] > num418)
							{
								num417 = num420;
								num418 = Main.projectile[num420].ai[1];
							}
						}
					}
					if (num416 > 2)
					{
						Main.projectile[num417].netUpdate = true;
						Main.projectile[num417].ai[1] = 36000f;
						return;
					}
				}
			}

			timer--;
			if (timer == 0)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("WitherShot"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
				timer = 120;
			}
			projectile.frameCounter++;
			if (projectile.frameCounter > 8)
			{
				projectile.frameCounter = 0;
				projectile.frame++;
				if (projectile.frame > 5)
				{
					projectile.frame = 0;
				}
			}
			projectile.ai[1] += 1f;
			if (projectile.ai[1] >= 7200f)
			{
				projectile.alpha += 5;
				if (projectile.alpha > 255)
				{
					projectile.alpha = 255;
					projectile.Kill();
				}
			}
		}
	}
}
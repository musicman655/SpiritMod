using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Projectiles.Bullet
{
	public class CrimBullet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crim Bullet");
		}

		//Warning : it's not my code. It's SpiritMod code. so i donnt fully understand it
		public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 20;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.alpha = 255;
			projectile.timeLeft = 240;
			aiType = ProjectileID.Bullet;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (target.lifeMax <= 5 || target.dontTakeDamage || target.friendly || target.immortal)
				return;
			Projectile.NewProjectile(target.Center, Vector2.Zero,
				305, 0, 0f, projectile.owner, projectile.owner, (damage + 5)/20);
		}

		public override bool PreAI()
		{
			int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height,
				5, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
			Main.dust[dust].scale = 2f;
			Main.dust[dust].noGravity = true;
			Main.dust[dust].noLight = true;

			return false;
		}

	}
}

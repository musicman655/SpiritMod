﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
namespace SpiritMod.Projectiles.Summon
{
    class ElectricGunProjectile : ModProjectile
    {
        int timer = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arcbolt");
        }

        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 59;
            projectile.height = 8;
            projectile.minion = true;
            projectile.width = 8;
            projectile.alpha = 255;
            projectile.hide = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = false;
            projectile.localNPCHitCooldown = 1;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
        }
        int counter;
        int numY;
        public override void AI()
        {
            int num = 5;
            for (int k = 0; k < 6; k++)
            {
                int index2 = Dust.NewDust(projectile.position, 1, 1, 226, 0.0f, 0.0f, 0, new Color(), .8f);
                Main.dust[index2].position = projectile.Center - projectile.velocity / num * (float)k;
                Main.dust[index2].scale = .5f;
                Main.dust[index2].velocity *= 0f;
                Main.dust[index2].noGravity = true;
                Main.dust[index2].noLight = false;
                Main.dust[index2].fadeIn = (float)(100 + projectile.owner);

            }
            numY = -8;
            if (projectile.timeLeft % 2 == 0)
            {
                projectile.velocity.Y = projectile.velocity.Y - numY;
            }
            else
            {
                projectile.velocity.Y = projectile.velocity.Y + numY + .5f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 10; i++)
            {
                int num = Dust.NewDust(target.position, target.width, target.height, 226, 0f, -2f, 0, default(Color), 1.952f);
                Main.dust[num].noGravity = true;
                Main.dust[num].position.X += Main.rand.Next(-50, 51) * .05f - 1.5f;
                Main.dust[num].position.Y += Main.rand.Next(-50, 51) * .05f - 1.5f;
                Main.dust[num].scale *= .25f;
                if (Main.dust[num].position != target.Center)
                    Main.dust[num].velocity = target.DirectionTo(Main.dust[num].position) * 3f;
            }
            Player player = Main.player[projectile.owner];
            target.AddBuff(mod.BuffType("ElectricSummonTag"), 240, true);
            int num1 = -1;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(player, false) && Main.npc[i] == target)
                {
                    num1 = i;
                }
            }
            {
                player.MinionAttackTargetNPC = num1;
            }
        }
	}
}

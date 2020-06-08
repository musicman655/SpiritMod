using SpiritMod.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritMod.Projectiles.Thrown
{
    public class CryoKnife : ModProjectile
    {
        public static int _type;

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Cryolite Knife");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults() {
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = 113;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 4;
            projectile.timeLeft = 600;
            projectile.alpha = 255;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.ThrowingKnife;
        }

        public override void Kill(int timeLeft) {
            if(Main.rand.Next(0, 4) == 0)
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, Items.Weapon.Thrown.CryoKnife._type, 1, false, 0, false, false);

            for(int i = 0; i < 5; i++) {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 180);
                Main.dust[d].scale = .5f;
            }
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            if(Main.rand.Next(4) == 0)
                target.AddBuff(ModContent.BuffType<CryoCrush>(), 240);
        }
        public override bool PreAI() {
            projectile.rotation += 0.1f;
            return true;
        }
    }
}
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using DataInvoice.Manager.Models;
using Microsoft.Owin.Security.Facebook;
using System.Threading.Tasks;
using Owin.Security.Providers.LinkedIn;
using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.Identity;

namespace DataInvoice.Manager
{
    public partial class Startup
    {
        // Pour plus d’informations sur la configuration de l’authentification, rendez-vous sur http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configurer le contexte de base de données, le gestionnaire des utilisateurs et le gestionnaire des connexions pour utiliser une instance unique par demande
          //  app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Autoriser l’application à utiliser un cookie pour stocker des informations pour l’utilisateur connecté
            // et pour utiliser un cookie à des fins de stockage temporaire des informations sur la connexion utilisateur avec un fournisseur de connexion tiers
            // Configurer le cookie de connexion
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Permet à l'application de valider le timbre de sécurité quand l'utilisateur se connecte.
                    // Cette fonction de sécurité est utilisée quand vous changez un mot de passe ou ajoutez une connexion externe à votre compte.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Permet à l'application de stocker temporairement les informations utilisateur lors de la vérification du second facteur dans le processus d'authentification à 2 facteurs.
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Permet à l'application de mémoriser le second facteur de vérification de la connexion, un numéro de téléphone ou un e-mail par exemple.
            // Lorsque vous activez cette option, votre seconde étape de vérification pendant le processus de connexion est mémorisée sur le poste à partir duquel vous vous êtes connecté.
            // Ceci est similaire à l'option RememberMe quand vous vous connectez.
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Supprimer les commentaires des lignes suivantes pour autoriser la connexion avec des fournisseurs de connexions tiers
            app.UseMicrosoftAccountAuthentication(new Microsoft.Owin.Security.MicrosoftAccount.MicrosoftAccountAuthenticationOptions()
            {
                ClientId = "21de8c24-d82a-485b-82fc-f6c7ee3134d6",//"3b55a474-8fe6-40e5-8a41-3dc438faae30",
                ClientSecret = "1F5A422CEE40E65C0F0DA82EB323D6CD14992616"//"AAC7674E0310E4F4A91C74D1B156FD3AFD7AF92C"
            });
            app.UseLinkedInAuthentication(
               clientId: "81khfzo3i1t07r",
               clientSecret: "bpfZQA0Ta0syqXgI");
            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //      appId: "241505719683857",
            //   appSecret: "ea5114b16710daf0f0af93b997ea3764");
            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AppId = "241505719683857",
                AppSecret = "ea5114b16710daf0f0af93b997ea3764",
                Scope = { "email" },
                Provider = new FacebookAuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                        return Task.FromResult(true);
                    }
                }
            });
            app.UseMicrosoftAccountAuthentication(new Microsoft.Owin.Security.MicrosoftAccount.MicrosoftAccountAuthenticationOptions()
            {
                ClientId = "3b55a474-8fe6-40e5-8a41-3dc438faae30",
                ClientSecret = "AAC7674E0310E4F4A91C74D1B156FD3AFD7AF92C"
            });
            //appId: "1901160633460196",
            //appSecret: "16cedaa359a9ef207d1901aa368c7c75");
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "870895588425-fhsqkj9rg21406cujfjlkijh2r0gdmua.apps.googleusercontent.com",
                ClientSecret = "6zuKZE3OLDSCxyEC0UEnipR7"
            });
        }
    }
}
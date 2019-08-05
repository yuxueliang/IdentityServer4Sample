using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Mvc.Services
{
    public class ConsentService
    {

        private readonly IClientStore _clientStore;

        private readonly IResourceStore _resourceStore;

        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        public ConsentService(IClientStore clientStore, IResourceStore resourceStore, IIdentityServerInteractionService identityServerInteractionService)
        {
            _clientStore = clientStore;
            _resourceStore = resourceStore;
            _identityServerInteractionService = identityServerInteractionService;
        }

        

        private ConsentViewModel CreateConsentViewModel(AuthorizationRequest request, Client client, Resources resources)
        {
            var vm = new ConsentViewModel();
            vm.ClientName = client.ClientName;
            vm.ClientLogoUrl = client.LogoUri;
            vm.ClientUrl = client.ClientUri;
            vm.RememberConsent = client.AllowRememberConsent;

            vm.IdentityScopes = resources.IdentityResources.Select(i => CreateScopeViewModel(i));
            vm.ResourceScopes = resources.ApiResources.SelectMany(i => i.Scopes).Select(i => CreateScopeViewModel(i));

            return vm;
        }

        private ScopeViewModel CreateScopeViewModel(IdentityResource identityResource)
        {
            return new ScopeViewModel
            {
                Name = identityResource.Name,
                DisplayName = identityResource.DisplayName,
                Description = identityResource.Description,
                Checked = identityResource.Required,
                Required = identityResource.Required,
                Emphasize = identityResource.Emphasize
            };
        }

        private ScopeViewModel CreateScopeViewModel(Scope scope)
        {
            return new ScopeViewModel
            {
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                Checked = scope.Required,
                Required = scope.Required,
                Emphasize = scope.Emphasize
            };
        }


        public async Task<ConsentViewModel> BuildConsentViewModelAsync(string returnUrl)
        {
            var request = await _identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            if (request == null) return null;
            var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
            var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);

            var vm = CreateConsentViewModel(request, client, resources);

            vm.ReturnUrl = returnUrl;

            return vm;
        }

        public async Task<ProcessConsentResult> ProcessConsentAsync(InputConsentViewModel model)
        {
            ConsentResponse consentResponse = null;

            var result = new ProcessConsentResult();
            if (model.Button == "no")
            {
                consentResponse = ConsentResponse.Denied;
            }
            else if (model.Button == "yes")
            {
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    consentResponse = new ConsentResponse
                    {
                        RememberConsent = model.RememberConsent,
                        ScopesConsented = model.ScopesConsented
                    };
                }
            }

            if (consentResponse != null)
            {
                var request = await _identityServerInteractionService.GetAuthorizationContextAsync(model.ReturnUrl);

                await _identityServerInteractionService.GrantConsentAsync(request, consentResponse);

                result.RedirectUrl = model.ReturnUrl;

            }

            result.ConsentViewModel = await BuildConsentViewModelAsync(model.ReturnUrl);
            return result;
        }
    }
}

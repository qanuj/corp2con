/* Setup Rounting For All Pages */
app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    var role = document.querySelector('html').dataset.role.toLowerCase();
    var common = role == "admin" ? "admin" : "common";

    // Redirect any unmatched url
    $urlRouterProvider.otherwise("/");

    $stateProvider.state('lost', {
        url: "/404",
        templateUrl: "app/views/common/404.html"
    });

    if (role == 'public') {
        $stateProvider
        .state('home', {
            url: "/",
            templateUrl: "app/views/" + role + "/search.html",
            data: { pageTitle: 'Search' },
            controller: role + "SearchController"
        })
        .state('company', {
            url: "/company/{id:int}",
            templateUrl: "app/views/" + role + "/company.html",
            data: { pageTitle: 'Company' },
            controller: role + "CompanyController"
        })
        .state('job', {
            url: "/{id:int}",
            templateUrl: "app/views/" + role + "/job.html",
            data: { pageTitle: 'Job' },
            controller: role + "JobController"
        });
    } else {
        // Dashboard
        $stateProvider.state('home', {
            url: "/",
            templateUrl: "app/views/" + role + "/dashboard.html",
            data: { pageTitle: 'Dashboard' },
            controller: role + "DashboardController"
            
        })
        .state('search', {
            url: "/search",
            templateUrl: "app/views/" + role + "/search.html",
            data: { pageTitle: 'Search' },
            controller: role + "SearchController"
            
        })
        .state('searchFolder', {
            url: "/search/folder/:folder",
            templateUrl: "app/views/" + role + "/search.html",
            data: { pageTitle: 'Search' },
            controller: role + "SearchController"
           
        })
        .state('searchIdea', {
            url: "/search/:idea",
            templateUrl: "app/views/" + role + "/search.html",
            data: { pageTitle: 'Search' },
            controller: role + "SearchController"
            
        })
        .state('searchPaged', {
            url: "/search/:idea/:page",
            templateUrl: "app/views/" + role + "/search.html",
            data: { pageTitle: 'Search' },
            controller: role + "SearchController"
            
        })
        .state('profile', {
            url: "/profile",
            templateUrl: "app/views/" + role + "/profile.html",
            data: { pageTitle: 'Profile' },
            controller: role + "ProfileController"
            
        })
        .state('billing', {
            url: "/billing",
            templateUrl: "app/views/" + common + "/billing.html",
            data: { pageTitle: 'Billing' },
            controller: role == "admin" ? "adminBillingController" : "billingController"
            
        })
        .state('invoice', {
            url: "/invoice/:id",
            templateUrl: "app/views/" + common + "/invoice.html",
            data: { pageTitle: 'Invoice' },
            controller: role == "admin" ? "adminInvoiceController" : "invoiceController"
            
        })
        .state('credit', {
            url: "/billing/credit",
            templateUrl: "app/views/" + common + "/addCredit.html",
            data: { pageTitle: 'Add Credit' },
            controller: role == "admin" ? "adminCreditController" : "creditController"
            
        })
        .state('profileEdit', {
            url: "/profile/edit",
            templateUrl: "app/views/" + role + "/editProfile.html",
            data: { pageTitle: 'Edit Profile' },
            controller: role + "EditProfileController"
            
        });
    }

    if (role == 'contractor') {
        $stateProvider
            .state('job', {
                url: "/job/:id",
                templateUrl: "app/views/" + role + "/job.html",
                data: { pageTitle: 'Job' },
                controller: role + "JobController"
                
            })
            .state('applications', {
                url: "/applications",
                templateUrl: "app/views/" + role + "/applications.html",
                data: { pageTitle: 'Applications' },
                controller: role + "ApplicationsController"
                
            })
            .state('schedule', {
                url: "/schedule",
                templateUrl: "app/views/" + role + "/schedule.html",
                data: { pageTitle: 'Job' },
                controller: role + "ScheduleController"
                
            })
            .state('favorite', {
                url: "/favorite",
                templateUrl: "app/views/" + role + "/applications.html",
                data: { pageTitle: 'Job' },
                controller: role + "FavoriteController"
                
            })
            .state('company', {
                url: "/company/:id",
                templateUrl: "app/views/" + role + "/company.html",
                data: { pageTitle: 'Job' },
                controller: role + "CompanyProfileController"
                
            })
            .state('promote', {
                url: "/promote",
                templateUrl: "app/views/" + role + "/promote.html",
                data: { pageTitle: 'Promote Profile' },
                controller: role + "PromoteController"
                
            });
    } else if (role == 'company') {
        $stateProvider
            .state('bench', {
                url: "/bench",
                templateUrl: "app/views/" + role + "/bench.html",
                data: { pageTitle: 'Bench' },
                controller: role + "BenchController"
                
            })
            .state('invite', {
                url: "/invite",
                templateUrl: "app/views/" + role + "/invite.html",
                data: { pageTitle: 'Invite' },
                controller: role + "InviteController"
                
            })
            .state('jobs', {
                url: "/jobs",
                templateUrl: "app/views/" + role + "/jobs.html",
                data: { pageTitle: 'Jobs' },
                controller: role + "JobsController"
                
            })
            .state('job', {
                url: "/job/{id:int}",
                templateUrl: "app/views/" + role + "/job.html",
                data: { pageTitle: 'Job' },
                controller: role + "JobController"
                
            })
            .state('jobEdit', {
                url: "/job/edit/:id",
                templateUrl: "app/views/" + role + "/editOrCreateJob.html",
                data: { pageTitle: 'Job Edit' },
                controller: role + "EditOrCreateJobController"
                
            })
            .state('jobNew', {
                url: "/job/new",
                templateUrl: "app/views/" + role + "/editOrCreateJob.html",
                data: { pageTitle: 'New Job' },
                controller: role + "EditOrCreateJobController"
                
            })
            .state('applicationFolder', {
                url: "/job/applications/:id/:folder",
                templateUrl: "app/views/" + role + "/applications.html",
                data: { pageTitle: 'Applications' },
                controller: role + "ApplicationsController"
                
            })
            .state('jobApplication', {
                url: "/job/applications/:id",
                templateUrl: "app/views/" + role + "/applications.html",
                data: { pageTitle: 'Applications' },
                controller: role + "ApplicationsController"
                
            })
            .state('contractor', {
                url: "/contractor/:id",
                templateUrl: "app/views/" + role + "/contractor.html",
                data: { pageTitle: 'Contractor' },
                controller: role + "ContractorController"
                
            })
            .state('promotejob', {
                url: "/promote/job/:id",
                templateUrl: "app/views/" + role + "/promotejob.html",
                data: { pageTitle: 'Job' },
                controller: role + "PromoteJobController"
                
            })
            .state('promote', {
                url: "/promote",
                templateUrl: "app/views/" + role + "/promote.html",
                data: { pageTitle: 'Profile' },
                controller: role + "PromoteController"
                
            });
    } else if (role == 'admin') {
        $stateProvider
            .state('industry', {
                url: "/industry",
                templateUrl: "app/views/" + role + "/master.html",
                data: { pageTitle: 'Industry',module:'industry' },
                controller: role + "MasterController"
                
            })
            .state('functional', {
                url: "/functional",
                templateUrl: "app/views/" + role + "/master.html",
                data: { pageTitle: 'Functional Area',module:'functional' },
                controller: role + "MasterController"
                
            })
            .state('skills', {
                url: "/skills",
                templateUrl: "app/views/" + role + "/master.html",
                data: { pageTitle: 'Skills',module:'skill' },
                controller: role + "MasterController"
                
            })
            .state('country', {
                url: "/country",
                templateUrl: "app/views/" + role + "/master.html",
                data: { pageTitle: 'Countries',module:'country' },
                controller: role + "MasterController"
                
            })
            .state('city', {
                url: "/location",
                templateUrl: "app/views/" + role + "/master.html",
                data: { pageTitle: 'Locations' ,module:'location' },
                controller: role + "MasterController"
                
            })
            .state('invite', {
                url: "/schedule",
                templateUrl: "app/views/" + role + "/invite.html",
                data: { pageTitle: 'Invite' },
                controller: role + "InviteController"
               
            })
            .state('feedback', {
                url: "/inbox/feedback",
                templateUrl: "app/views/" + role + "/inbox.html",
                data: { pageTitle: 'site feedback',module:'feedback' },
                controller: role + "InboxController"
                
            })
            .state('config', {
                url: "/config",
                templateUrl: "app/views/" + role + "/config.html",
                data: { pageTitle: 'Configuration' },
                controller: role + "ConfigController"
                
            });
    }

}]);
/* Setup Rounting For All Pages */
app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    var role = document.querySelector('html').dataset.role.toLowerCase();

    // Redirect any unmatched url
    $urlRouterProvider.otherwise("/");

    $stateProvider
        // Dashboard
        .state('dashboard', {
            url: "/",
            templateUrl: "app/views/" + role + "/dashboard.html",
            data: { pageTitle: 'Dashboard' },
            controller: role + "DashboardController",
            resolve: {
                deps: [
                    '$ocLazyLoad', function($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: 'app',
                            insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                            files: [
                                '/assets/admin/pages/css/profile.css'
                            ]
                        });
                    }
                ]
            }
        })

        // AngularJS plugins
        .state('search', {
            url: "/search/:idea?/:page?",
            templateUrl: "app/views/" + role + "/search.html",
            data: { pageTitle: 'Search' },
            controller: role + "SearchController",
            resolve: {}
        })
        .state('profile', {
            url: "/profile",
            templateUrl: "app/views/" + role + "/profile.html",
            data: { pageTitle: 'Search' },
            controller: role + "ProfileController",
            resolve: {}
        })
        .state('profile.edit', {
            url: "/edit",
            templateUrl: "app/views/" + role + "/editProfile.html",
            data: { pageTitle: 'Search' },
            controller: role + "EditProfileController",
            resolve: {}
        })
        .state('applications', {
            url: "/applications",
            templateUrl: "app/views/" + role + "/applications.html",
            data: { pageTitle: 'Applications' },
            controller: role + "ApplicationsController",
            resolve: {}
        });

    if (role == 'contractor') {
        $stateProvider
            .state('job', {
                url: "/job/:id",
                templateUrl: "app/views/" + role + "/job.html",
                data: { pageTitle: 'Job' },
                controller: role + "JobController",
                resolve: {}
            })
            .state('schedule', {
                url: "/schedule",
                templateUrl: "app/views/" + role + "/schedule.html",
                data: { pageTitle: 'Job' },
                controller: role + "ScheduleController",
                resolve: {}
            })
            .state('favorite', {
                url: "/favorite",
                templateUrl: "app/views/" + role + "/favorite.html",
                data: { pageTitle: 'Job' },
                controller: role + "FavoriteController",
                resolve: {}
            })
            .state('company', {
                url: "/:id",
                templateUrl: "app/views/" + role + "/company.html",
                data: { pageTitle: 'Job' },
                controller: role + "CompanyProfileController",
                resolve: {}
            });
    }else if (role == 'company') {
        $stateProvider
            .state('bench', {
                url: "/bench",
                templateUrl: "app/views/" + role + "/bench.html",
                data: { pageTitle: 'Bench' },
                controller: role + "BenchController",
                resolve: {}
            })
            .state('invite', {
                url: "/schedule",
                templateUrl: "app/views/" + role + "/invite.html",
                data: { pageTitle: 'Invite' },
                controller: role + "InviteController",
                resolve: {}
            })
            .state('billing', {
                url: "/billing/:page?",
                templateUrl: "app/views/" + role + "/billing.html",
                data: { pageTitle: 'Billing' },
                controller: role + "BillingController",
                resolve: {}
            })
            .state('jobs', {
                url: "/jobs/:page?",
                templateUrl: "app/views/" + role + "/jobs.html",
                data: { pageTitle: 'Jobs' },
                controller: role + "JobsController",
                resolve: {}
            })
            .state('job', {
                url: "/job",
                'abstract': true
            })
            .state('job.one', {
                url: "/:id",
                templateUrl: "app/views/" + role + "/job.html",
                data: { pageTitle: 'Job' },
                controller: role + "JobController",
                resolve: {}
            })
            .state('job.edit', {
                url: "/edit/:id",
                templateUrl: "app/views/" + role + "/editOrCreateJob.html",
                data: { pageTitle: 'Job Edit' },
                controller: role + "EditOrCreateJobController",
                resolve: {}
            })
            .state('job.new', {
                url: "/new",
                templateUrl: "app/views/" + role + "/editOrCreateJob.html",
                data: { pageTitle: 'New Job' },
                controller: role + "EditOrCreateJobController",
                resolve: {}
            })
            .state('job.applications', {
                url: "/:id/applications/:folder?",
                templateUrl: "app/views/" + role + "/contractorApplication.html",
                data: { pageTitle: 'New Job' },
                controller: role + "ApplicationsController",
                resolve: {}
            })
            .state('job.application', {
                url: "/applications/:id",
                templateUrl: "app/views/" + role + "/applications.html",
                data: { pageTitle: 'New Job' },
                controller: role + "ApplicationsController",
                resolve: {}
            })
            .state('contractor', {
                url: "/contractor/:id",
                templateUrl: "app/views/" + role + "/contractorApplication.html",
                data: { pageTitle: 'Contractor' },
                controller: role + "ContractorApplicationController",
                resolve: {}
            });
    } else if (role == 'admin') {
        $stateProvider
            .state('industry', {
                url: "/industry",
                templateUrl: "app/views/" + role + "/industry.html",
                data: { pageTitle: 'Industry' },
                controller: role + "IndustryController",
                resolve: {}
            })
            .state('functional', {
                url: "/functional",
                templateUrl: "app/views/" + role + "/functional.html",
                data: { pageTitle: 'Functional' },
                controller: role + "FunctionalController",
                resolve: {}
            })
            .state('skills', {
                url: "/skills",
                templateUrl: "app/views/" + role + "/skills.html",
                data: { pageTitle: 'Skills' },
                controller: role + "SkillsController",
                resolve: {}
            })
            .state('invite', {
                url: "/schedule",
                templateUrl: "app/views/" + role + "/invite.html",
                data: { pageTitle: 'Invite' },
                controller: role + "InviteController",
                resolve: {}
            })
            .state('billing', {
                url: "/billing/:page?",
                templateUrl: "app/views/" + role + "/billing.html",
                data: { pageTitle: 'Billing' },
                controller: role + "BillingController",
                resolve: {}
            })
    }

}]);
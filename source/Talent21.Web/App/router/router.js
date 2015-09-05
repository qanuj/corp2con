/* Setup Rounting For All Pages */
app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    var role = document.querySelector('html').dataset.role.toLowerCase();

    // Redirect any unmatched url
    $urlRouterProvider.otherwise("/");

    var datePickerDependency = {
        deps: [
            '$ocLazyLoad', function($ocLazyLoad) {
                return $ocLazyLoad.load({
                    name: 'app',
                    insertBefore: '#ng_load_plugins_before',
                    files: [
                        '/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css',
                        '/assets/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css',
                        '/assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css',
                        '/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js',
                        '/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.js',
                        '/assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js'
                    ]
                });
            }
        ]
    };

    var searchDependency = {
        deps: [
            '$ocLazyLoad', function($ocLazyLoad) {
                return $ocLazyLoad.load({
                    name: 'app',
                    insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                    files: [
                        '/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css',
                        '/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js'
                    ]
                });
            }
        ]
    };

    $stateProvider
        // Dashboard
        .state('dashboard', {
            url: "/",
            templateUrl: "app/views/" + role + "/dashboard.html",
            data: { pageTitle: 'Dashboard' },
            controller: role + "DashboardController",
            resolve: {
                deps: [
                    '$ocLazyLoad', function ($ocLazyLoad) {
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
            url: "/search",
            templateUrl: "app/views/" + role + "/search.html",
            data: { pageTitle: 'Search' },
            controller: role + "SearchController",
            resolve: searchDependency
        })
        .state('searchIdea', {
            url: "/search/:idea",
            templateUrl: "app/views/" + role + "/search.html",
            data: { pageTitle: 'Search' },
            controller: role + "SearchController",
            resolve: searchDependency
        })
        .state('searchPaged', {
            url: "/search/:idea/:page",
            templateUrl: "app/views/" + role + "/search.html",
            data: { pageTitle: 'Search' },
            controller: role + "SearchController",
            resolve: searchDependency
        })
        .state('profile', {
            url: "/profile",
            templateUrl: "app/views/" + role + "/profile.html",
            data: { pageTitle: 'Profile' },
            controller: role + "ProfileController",
            resolve: {}
        })
        .state('billing', {
            url: "/billing",
            templateUrl: "app/views/common/billing.html",
            data: { pageTitle: 'Billing' },
            controller: "billingController",
            resolve: {}
        })
        .state('invoice', {
            url: "/invoice/:id",
            templateUrl: "app/views/common/invoice.html",
            data: { pageTitle: 'Invoice' },
            controller: "invoiceController",
            resolve: {}
        })
        .state('credit', {
            url: "/billing/credit",
            templateUrl: "app/views/common/addCredit.html",
            data: { pageTitle: 'Add Credit' },
            controller: "creditController",
            resolve: {}
        })
        .state('profileEdit', {
            url: "/profile/edit",
            templateUrl: "app/views/" + role + "/editProfile.html",
            data: { pageTitle: 'Edit Profile' },
            controller: role + "EditProfileController",
            resolve: {}
        })
        .state('promote', {
            url: "/promote/job/:id",
            templateUrl: "app/views/" + role + "/promote.html",
            data: { pageTitle: 'Job' },
            controller: role + "PromoteController",
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
            .state('applications', {
                url: "/applications",
                templateUrl: "app/views/" + role + "/applications.html",
                data: { pageTitle: 'Applications' },
                controller: role + "ApplicationsController",
                resolve: {}
            })
            .state('schedule', {
                url: "/schedule",
                templateUrl: "app/views/" + role + "/schedule.html",
                data: { pageTitle: 'Job' },
                controller: role + "ScheduleController",
                resolve: datePickerDependency
            })
            .state('favorite', {
                url: "/favorite",
                templateUrl: "app/views/" + role + "/favorite.html",
                data: { pageTitle: 'Job' },
                controller: role + "FavoriteController",
                resolve: {}
            })
            .state('company', {
                url: "/company/:id",
                templateUrl: "app/views/" + role + "/company.html",
                data: { pageTitle: 'Job' },
                controller: role + "CompanyProfileController",
                resolve: {}
            })
            .state('promote', {
                url: "/promote/job/:id",
                templateUrl: "app/views/" + role + "/promote.html",
                data: { pageTitle: 'Job' },
                controller: role + "PromoteController",
                resolve: {}
            });
    } else if (role == 'company') {
        $stateProvider
            .state('bench', {
                url: "/bench",
                templateUrl: "app/views/" + role + "/bench.html",
                data: { pageTitle: 'Bench' },
                controller: role + "BenchController",
                resolve: {}
            })
            .state('invite', {
                url: "/invite",
                templateUrl: "app/views/" + role + "/invite.html",
                data: { pageTitle: 'Invite' },
                controller: role + "InviteController",
                resolve: {}
            })
            .state('jobs', {
                url: "/jobs",
                templateUrl: "app/views/" + role + "/jobs.html",
                data: { pageTitle: 'Jobs' },
                controller: role + "JobsController",
                resolve: {}
            })
            .state('jobOne', {
                url: "/job/{id:int}",
                templateUrl: "app/views/" + role + "/job.html",
                data: { pageTitle: 'Job' },
                controller: role + "JobController",
                resolve: {}
            })
            .state('jobEdit', {
                url: "/job/edit/:id",
                templateUrl: "app/views/" + role + "/editOrCreateJob.html",
                data: { pageTitle: 'Job Edit' },
                controller: role + "EditOrCreateJobController",
                resolve: datePickerDependency
            })
            .state('jobNew', {
                url: "/job/new",
                templateUrl: "app/views/" + role + "/editOrCreateJob.html",
                data: { pageTitle: 'New Job' },
                controller: role + "EditOrCreateJobController",
                resolve: datePickerDependency
            })
            .state('jobApplications', {
                url: "/job/:id/applications/:folder?",
                templateUrl: "app/views/" + role + "/contractorApplication.html",
                data: { pageTitle: 'New Job' },
                controller: role + "ApplicationsController",
                resolve: {}
            })
            .state('jobApplication', {
                url: "/job/applications/:id",
                templateUrl: "app/views/" + role + "/applications.html",
                data: { pageTitle: 'New Job' },
                controller: role + "ApplicationsController",
                resolve: {}
            })
            .state('contractor', {
                url: "/contractor/:id",
                templateUrl: "app/views/" + role + "/contractor.html",
                data: { pageTitle: 'Contractor' },
                controller: role + "ContractorController",
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
            });
    }

}]);
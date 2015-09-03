/* Setup Rounting For All Pages */
app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    var role = document.querySelector('html').dataset.role;

    // Redirect any unmatched url
    $urlRouterProvider.otherwise("/");

    $stateProvider
        // Dashboard
        .state('dashboard', {
            url: "/",
            templateUrl: "app/views/contractor/dashboard.html",
            data: { pageTitle: 'Admin Dashboard Template' },
            controller: "contractorDashboardController",
            resolve: {
                deps: [
                    '$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: 'app',
                            insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                            files: [
                                '/assets/global/plugins/morris/morris.css',
                                '/assets/admin/pages/css/tasks.css',
                                '/assets/admin/pages/css/profile.css',
                                '/assets/global/plugins/morris/morris.min.js',
                                '/assets/global/plugins/morris/raphael-min.js',
                                '/assets/global/plugins/jquery.sparkline.min.js',
                                '/assets/admin/pages/scripts/index3.js',
                                '/assets/admin/pages/scripts/tasks.js'
                            ]
                        });
                    }
                ]
            }
        })

    // AngularJS plugins
    .state('fileupload', {
        url: "/file_upload.html",
        templateUrl: "views/file_upload.html",
        data: { pageTitle: 'AngularJS File Upload' },
        controller: "GeneralPageController",
        resolve: {
            deps: [
                '$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'angularFileUpload',
                            files: [
                                '/assets/global/plugins/angularjs/plugins/angular-file-upload/angular-file-upload.min.js',
                            ]
                        }, {
                            name: 'app',
                            files: [
                                'js/controllers/GeneralPageController.js'
                            ]
                        }
                    ]);
                }
            ]
        }
    })

    // UI Select
    .state('uiselect', {
        url: "/ui_select.html",
        templateUrl: "views/ui_select.html",
        data: { pageTitle: 'AngularJS Ui Select' },
        controller: "UISelectController",
        resolve: {
            deps: [
                '$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'ui.select',
                            insertBefore: '#ng_load_plugins_before', // load the above css files before '#ng_load_plugins_before'
                            files: [
                                '/assets/global/plugins/angularjs/plugins/ui-select/select.min.css',
                                '/assets/global/plugins/angularjs/plugins/ui-select/select.min.js'
                            ]
                        }, {
                            name: 'app',
                            files: [
                                'js/controllers/UISelectController.js'
                            ]
                        }
                    ]);
                }
            ]
        }
    })

    // UI Bootstrap
    .state('uibootstrap', {
        url: "/ui_bootstrap.html",
        templateUrl: "views/ui_bootstrap.html",
        data: { pageTitle: 'AngularJS UI Bootstrap' },
        controller: "GeneralPageController",
        resolve: {
            deps: [
                '$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'app',
                            files: [
                                'js/controllers/GeneralPageController.js'
                            ]
                        }
                    ]);
                }
            ]
        }
    })

    // Tree View
    .state('tree', {
        url: "/tree",
        templateUrl: "views/tree.html",
        data: { pageTitle: 'jQuery Tree View' },
        controller: "GeneralPageController",
        resolve: {
            deps: [
                '$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'app',
                            insertBefore: '#ng_load_plugins_before', // load the above css files before '#ng_load_plugins_before'
                            files: [
                                '/assets/global/plugins/jstree/dist/themes/default/style.min.css',
                                '/assets/global/plugins/jstree/dist/jstree.min.js',
                                '/assets/admin/pages/scripts/ui-tree.js',
                                'js/controllers/GeneralPageController.js'
                            ]
                        }
                    ]);
                }
            ]
        }
    })

    // Form Tools
    .state('formtools', {
        url: "/form-tools",
        templateUrl: "views/form_tools.html",
        data: { pageTitle: 'Form Tools' },
        controller: "GeneralPageController",
        resolve: {
            deps: [
                '$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'app',
                            insertBefore: '#ng_load_plugins_before', // load the above css files before '#ng_load_plugins_before'
                            files: [
                                '/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css',
                                '/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css',
                                '/assets/global/plugins/jquery-tags-input/jquery.tagsinput.css',
                                '/assets/global/plugins/bootstrap-markdown/css/bootstrap-markdown.min.css',
                                '/assets/global/plugins/typeahead/typeahead.css',
                                '/assets/global/plugins/fuelux/js/spinner.min.js',
                                '/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js',
                                '/assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js',
                                '/assets/global/plugins/jquery.input-ip-address-control-1.0.min.js',
                                '/assets/global/plugins/bootstrap-pwstrength/pwstrength-bootstrap.min.js',
                                '/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js',
                                '/assets/global/plugins/jquery-tags-input/jquery.tagsinput.min.js',
                                '/assets/global/plugins/bootstrap-maxlength/bootstrap-maxlength.min.js',
                                '/assets/global/plugins/bootstrap-touchspin/bootstrap.touchspin.js',
                                '/assets/global/plugins/typeahead/handlebars.min.js',
                                '/assets/global/plugins/typeahead/typeahead.bundle.min.js',
                                '/assets/admin/pages/scripts/components-form-tools.js',
                                'js/controllers/GeneralPageController.js'
                            ]
                        }
                    ]);
                }
            ]
        }
    })

    // Date & Time Pickers
    .state('pickers', {
        url: "/pickers",
        templateUrl: "views/pickers.html",
        data: { pageTitle: 'Date & Time Pickers' },
        controller: "GeneralPageController",
        resolve: {
            deps: [
                '$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'app',
                            insertBefore: '#ng_load_plugins_before', // load the above css files before '#ng_load_plugins_before'
                            files: [
                                '/assets/global/plugins/clockface/css/clockface.css',
                                '/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css',
                                '/assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css',
                                '/assets/global/plugins/bootstrap-colorpicker/css/colorpicker.css',
                                '/assets/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css',
                                '/assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css',
                                '/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js',
                                '/assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js',
                                '/assets/global/plugins/clockface/js/clockface.js',
                                '/assets/global/plugins/bootstrap-daterangepicker/moment.min.js',
                                '/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.js',
                                '/assets/global/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.js',
                                '/assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js',
                                '/assets/admin/pages/scripts/components-pickers.js',
                                'js/controllers/GeneralPageController.js'
                            ]
                        }
                    ]);
                }
            ]
        }
    })

    // Custom Dropdowns
    .state('dropdowns', {
        url: "/dropdowns",
        templateUrl: "views/dropdowns.html",
        data: { pageTitle: 'Custom Dropdowns' },
        controller: "GeneralPageController",
        resolve: {
            deps: [
                '$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'app',
                            insertBefore: '#ng_load_plugins_before', // load the above css files before '#ng_load_plugins_before'
                            files: [
                                '/assets/global/plugins/bootstrap-select/bootstrap-select.min.css',
                                '/assets/global/plugins/select2/select2.css',
                                '/assets/global/plugins/jquery-multi-select/css/multi-select.css',
                                '/assets/global/plugins/bootstrap-select/bootstrap-select.min.js',
                                '/assets/global/plugins/select2/select2.min.js',
                                '/assets/global/plugins/jquery-multi-select/js/jquery.multi-select.js',
                                '/assets/admin/pages/scripts/components-dropdowns.js',
                                'js/controllers/GeneralPageController.js'
                            ]
                        }
                    ]);
                }
            ]
        }
    })

    // Advanced Datatables
    .state('datatablesAdvanced', {
        url: "/datatables/advanced.html",
        templateUrl: "views/datatables/advanced.html",
        data: { pageTitle: 'Advanced Datatables' },
        controller: "GeneralPageController",
        resolve: {
            deps: [
                '$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'app',
                        insertBefore: '#ng_load_plugins_before', // load the above css files before '#ng_load_plugins_before'
                        files: [
                            '/assets/global/plugins/select2/select2.css',
                            '/assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css',
                            '/assets/global/plugins/datatables/extensions/Scroller/css/dataTables.scroller.min.css',
                            '/assets/global/plugins/datatables/extensions/ColReorder/css/dataTables.colReorder.min.css',
                            '/assets/global/plugins/select2/select2.min.js',
                            '/assets/global/plugins/datatables/all.min.js',
                            'js/scripts/table-advanced.js',
                            'js/controllers/GeneralPageController.js'
                        ]
                    });
                }
            ]
        }
    })

    // Ajax Datetables
    .state('datatablesAjax', {
        url: "/datatables/ajax.html",
        templateUrl: "views/datatables/ajax.html",
        data: { pageTitle: 'Ajax Datatables' },
        controller: "GeneralPageController",
        resolve: {
            deps: [
                '$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'app',
                        insertBefore: '#ng_load_plugins_before', // load the above css files before '#ng_load_plugins_before'
                        files: [
                            '/assets/global/plugins/select2/select2.css',
                            '/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css',
                            '/assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css',
                            '/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js',
                            '/assets/global/plugins/select2/select2.min.js',
                            '/assets/global/plugins/datatables/all.min.js',
                            '/assets/global/scripts/datatable.js',
                            'js/scripts/table-ajax.js',
                            'js/controllers/GeneralPageController.js'
                        ]
                    });
                }
            ]
        }
    })

    // User Profile
    .state("profile", {
        url: "/profile",
        templateUrl: "views/profile/main.html",
        data: { pageTitle: 'User Profile' },
        controller: "UserProfileController",
        resolve: {
            deps: [
                '$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'app',
                        insertBefore: '#ng_load_plugins_before', // load the above css files before '#ng_load_plugins_before'
                        files: [
                            '/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css',
                            '/assets/admin/pages/css/profile.css',
                            '/assets/admin/pages/css/tasks.css',
                            '/assets/global/plugins/jquery.sparkline.min.js',
                            '/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js',
                            '/assets/admin/pages/scripts/profile.js',
                            'js/controllers/UserProfileController.js'
                        ]
                    });
                }
            ]
        }
    })

    // User Profile Dashboard
    .state("profile.dashboard", {
        url: "/dashboard",
        templateUrl: "views/profile/dashboard.html",
        data: { pageTitle: 'User Profile' }
    })

    // User Profile Account
    .state("profile.account", {
        url: "/account",
        templateUrl: "views/profile/account.html",
        data: { pageTitle: 'User Account' }
    })

    // User Profile Help
    .state("profile.help", {
        url: "/help",
        templateUrl: "views/profile/help.html",
        data: { pageTitle: 'User Help' }
    })

    // Todo
    .state('todo', {
        url: "/todo",
        templateUrl: "views/todo.html",
        data: { pageTitle: 'Todo' },
        controller: "TodoController",
        resolve: {
            deps: [
                '$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'app',
                        insertBefore: '#ng_load_plugins_before', // load the above css files before '#ng_load_plugins_before'
                        files: [
                            '/assets/global/plugins/bootstrap-datepicker/css/datepicker3.css',
                            '/assets/global/plugins/select2/select2.css',
                            '/assets/admin/pages/css/todo.css',
                            '/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js',
                            '/assets/global/plugins/select2/select2.min.js',
                            '/assets/admin/pages/scripts/todo.js',
                            'js/controllers/TodoController.js'
                        ]
                    });
                }
            ]
        }
    });

}]);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    var role = document.querySelector('html').dataset.role;

    if (role === 'Contractor') {
        $routeProvider
            .when('/', {
                templateUrl: '/App/views/contractor/dashboard.html',
                controller: 'contractorDashboardController'
            })
            .when('/profile', {
                templateUrl: '/app/views/contractor/profile.html',
                controller: 'contractorProfileController'
            })
            .when('/profile/edit', {
                templateUrl: '/app/views/contractor/editProfile.html',
                controller: 'contractorEditProfileController'
            })
             .when('/schedule', {
                 templateUrl: '/app/views/contractor/schedule.html',
                 controller: 'jobscheduleController'
             })
            .when('/search/:idea?/:page?', {
                templateUrl: '/app/views/contractor/search.html',
                controller: 'contractorSearchController'
            })
             .when('/applications/:page?', {
                 templateUrl: '/app/views/contractor/applications.html',
                 controller: 'contractorApplicationsController'
             })
            .when('/favorite', {
                templateUrl: '/app/views/contractor/favorite.html',
                controller: 'contractorFavoriteController'
            })
            .when('/job/:id', {
                templateUrl: '/app/views/contractor/job.html', //only for jobs about page.
                controller: 'contractorJobController'
            })
            .when('/job/application/favorite/:id', {
                templateUrl: '/app/views/contractor/favouritejobs.html',
                controller: 'contractorApplicationController'
            })

             .when('/company/:id', {
                 templateUrl: '/app/views/contractor/company.html',
                 controller: 'contractorCompanyProfileController'
             })
            .otherwise({ redirectTo: '/' });

    } else if (role === 'Admin') {
        $routeProvider
           .when('/', {
               templateUrl: '/app/views/admin/dashboard.html',
               controller: 'adminDashboardController'
           })
            .when('/industry', {
                templateUrl: '/app/views/admin/industry.html',
                controller: 'industryController'
            })
           .when('/functional', {
               templateUrl: '/app/views/admin/functional.html',
               controller: 'functionalController'
           })
             .when('/skills', {
                 templateUrl: '/app/views/system/skills.html',
                 controller: 'jobskillsController'
             })
           .otherwise({ redirectTo: '/' });
    } else if (role === 'Company') {
        $routeProvider
            .when('/', {
                templateUrl: '/app/views/company/dashboard.html',
                controller: 'companyDashboardController'
            })
            .when('/industry', {
                templateUrl: '/app/views/system/industry.html',
                controller: 'industryController'
            })
           .when('/functional', {
               templateUrl: '/app/views/system/functional.html',
               controller: 'functionalController'
           })
            .when('/search/:folder?', {
                templateUrl: '/app/views/company/search.html',
                controller: 'companySearchController'
            })
            .when('/bench', {
                templateUrl: '/app/views/company/bench.html',
                controller: 'companyBenchController'
            })
            .when('/invite', {
                templateUrl: '/app/views/company/invite.html',
                controller: 'companyInviteController'
            })
            .when('/profile', {
                templateUrl: '/app/views/company/profile.html',
                controller: 'companyProfileController'
            })
            .when('/profile/edit', {
                templateUrl: '/app/views/company/editProfile.html',
                controller: 'companyEditProfileController'
            })
            .when('/billing/:page?', {
                templateUrl: '/app/views/company/billing.html',
                controller: 'companyBillingController'
            })
            .when('/job/new', {
                templateUrl: '/app/views/company/editOrCreateJob.html',
                controller: 'editOrCreateJobController'
            })
            .when('/jobs/:page?', {
                templateUrl: '/app/views/company/jobs.html',
                controller: 'jobsController'
            })
            .when('/job/:id', {
                templateUrl: '/app/views/company/job.html', //only for jobs about page.
                controller: 'companyJobController'
            })
            .when('/contractor/:id', {
                templateUrl: '/app/views/company/contractor.html', //only for jobs about page.
                controller: 'companyContractorController'
            })
            .when('/job/edit/:id', {
                templateUrl: '/app/views/company/editOrCreateJob.html',
                controller: 'editOrCreateJobController'
            })
             .when('/job/application/:id', {
                 templateUrl: '/app/views/company/contractorApplication.html',
                 controller: 'contractorApplicationController'
             })
            .when('/job/:id/applications/:folder?', {
                templateUrl: '/app/views/company/applications.html',
                controller: 'companyApplicationsController'
            })
            .otherwise({ redirectTo: '/' });
    }

}]);
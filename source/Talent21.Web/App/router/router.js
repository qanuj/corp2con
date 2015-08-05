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
            .when('/search', {
                templateUrl: '/app/views/contractor/search.html',
                controller: 'contractorSearchController'
            })
             .when('/applications', {
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
            .when('/skills', {
                templateUrl: '/app/views/system/skills.html',
                controller: 'jobskillsController'
            })
           
             .when('/company/:id', {
                 templateUrl: '/app/views/company/profile.html',
                 controller: 'companyProfileController'
             })
            .otherwise({ redirectTo: '/' });

    } else if (role === 'Admin') {
        $routeProvider
           .when('/', {
               templateUrl: '/app/views/system/dashboard.html',
               controller: 'adminDashboardController'
           })
            .when('/industry', {
                templateUrl: '/app/views/system/industry.html',
                controller: 'industryController'
            })
           .when('/functional', {
               templateUrl: '/app/views/system/functional.html',
               controller: 'functionalController'
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
            .when('/search', {
                templateUrl: '/app/views/company/search.html',
                controller: 'companySearchController'
            })
            .when('/profile', {
                templateUrl: '/app/views/company/profile.html',
                controller: 'companyProfileController'
            })
            .when('/profile/edit', {
                templateUrl: '/app/views/company/editProfile.html',
                controller: 'companyEditProfileController'
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
            .when('/job/edit/:id', {
                templateUrl: '/app/views/company/editOrCreateJob.html',
                controller: 'editOrCreateJobController'
            })
             .when('/job/application/:id', {
                 templateUrl: '/app/views/company/contractorApplication.html',
                 controller: 'contractorApplicationController'
             })
            .when('/job/:id/applications', {
                templateUrl: '/app/views/company/applications.html',
                controller: 'companyApplicationsController'
            })
            .otherwise({ redirectTo: '/' });
    }

}]);
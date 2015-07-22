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
            .when('/favorite', {
                templateUrl: '/app/views/contractor/favorite.html',
                controller: 'contractorFavoriteController'
            })
              .when('/job/:id', {
                  templateUrl: '/app/views/contractor/job.html', //only for jobs about page.
                  controller: 'contractorJobController'
              })
            .when('/job/apply/:id', {
                templateUrl: '/app/views/contractor/applications.html',
                controller: 'contractorApplicationController'
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
    } else if (role === 'Company') {
        $routeProvider
            .when('/', {
                templateUrl: '/app/views/company/dashboard.html',
                controller: 'companyDashboardController'
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
            .when('/jobs', {
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
             .when('/contractor/:id', {
                 templateUrl: '/app/views/contractor/profile.html',
                 controller: 'contractorProfileController'
             })
             .when('/system/industry', {
                 templateUrl: '/app/views/system/industry.html',
                 controller: 'industryController'
             })
             .when('/system/functional', {
                 templateUrl: '/app/views/system/functional.html',
                 controller: 'functionalController'
             })
            .when('/system/industry/edit', {
                templateUrl: '/app/views/system/editindustry.html',
                controller: 'industryeditController'
            })
            .otherwise({ redirectTo: '/' });
    }

}]);
app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    var role = document.querySelector('html').dataset.role;
    console.log(role);
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
                controller: 'companySearchController'
            })
            .when('/applications', {
                templateUrl: '/app/views/contractor/applications.html',
                controller: 'contractorApplicationController'
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
            .when('/job/create', {
                templateUrl: '/app/views/company/createJob.html',
                controller: 'createJobController'
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
                templateUrl: '/app/views/company/editJob.html',
                controller: 'companyJobEditController'
            })
            .otherwise({ redirectTo: '/' });
    }

}]);
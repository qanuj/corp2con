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
              .when('/profile', {
                  templateUrl: '/app/views/contractor/profile.html',
                  controller: 'contractorProfileController'
              })
            .when('/profile/edit', {
                templateUrl: '/app/views/contractor/editProfile.html',
                controller: 'contractorEditProfileController'
            })
            .when('/search', {
                templateUrl: '/app/views/contractor/search.html',
                controller: 'contractorSearchController'
            })
            .when('/applications', {
                templateUrl: '/app/views/contractor/applications.html',
                controller: 'contractorApplicationController'
            })
              .when('/search', {
                  templateUrl: '/app/views/contractor/search.html',
                  controller: 'contractorSearchController'
              })
              .when('/jobschedule', {
                  templateUrl: '/app/views/contractor/jobschedule.html',
                  controller: 'jobscheduleController'
              })
            .otherwise({ redirectTo: '/' });
    } else if (role === 'Company') {
        $routeProvider
            .when('/', {
                templateUrl: '/App/views/company/dashboard.html',
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
            .when('/companylist', {
                templateUrl: '/app/views/Company/CompanyList.html',
                controller: 'companyListController'
            })
            .when('/joblisting', {
                templateUrl: '/app/views/job/jobListing.html',
                controller: 'jobListingController'
            })
            .when('/postjob', {
                templateUrl: '/app/views/job/PostJobs.html',
                controller: 'postjobsController'
            })
            .when('/myjobs', {
                templateUrl: '/app/views/job/jobs.html',
                controller: 'myJobsController'
            })
            .otherwise({ redirectTo: '/' });
    }

}]);
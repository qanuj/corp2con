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
            .when('/search', {
                templateUrl: '/app/views/contractor/search.html',
                controller: 'contractorSearchController'
            })
            .when('/applications', {
                templateUrl: '/app/views/contractor/applications.html',
                controller: 'contractorApplicationController'
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
            .when('/companylist', {
                templateUrl: '/app/views/Company/CompanyList.html',
                controller: 'companyListController'
            })
            .when('/postjobs', {
                templateUrl: '/app/views/job/postjobs.html',
                controller: 'postjobsController'
            })
            .when('/joblisting', {
                templateUrl: '/app/views/job/jobListing.html',
                controller: 'jobListingController'
            })
            .otherwise({ redirectTo: '/' });
    }

}]);
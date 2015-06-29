app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    var role = document.querySelector('html').dataset.role;
    console.log(role);
    if (role === 'Contractor') {
        $routeProvider
            .when('/', {
                templateUrl: '/App/views/contractor/dashboard.html',
                controller: 'contractorDashboardController'
            })
            .when('/others', {
                templateUrl: '/app/views/contractor/other.html',
                controller: 'otherController'
            })
            .when('/candidateprofile', {
                templateUrl: '/app/views/contractor/aboutcandidate.html',
                controller: 'candidateProfileController'
            })
            .when('/companylist', {
                templateUrl: '/app/views/company/CompanyList.html',
                controller: 'companyListController'
            })
            .otherwise({ redirectTo: '/' });
    } else if (role === 'Company') {
        $routeProvider
            .when('/', {
                templateUrl: '/App/views/company/dashboard.html',
                controller: 'companyDashboardController'
            })
            .when('/others', {
                templateUrl: '/app/views/Company/other.html',
                controller: 'otherController'
            })
            .when('/candidateprofile', {
                templateUrl: '/app/views/Company/candidateProfile.html',
                controller: 'candidateProfileController'
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
app.config(function ($routeProvider, $locationProvider) {
    var role = document.querySelector('html').dataset.role;
    console.log(role);
    if (role === 'Contractor') {
        $routeProvider
            .when('/', {
                templateUrl: '/App/views/candidate/aboutcandidate.html',
                controller: 'contractorProfileController'
            })
            .when('/others', {
                templateUrl: '/app/views/candidate/other.html',
                controller: 'otherController'
            })
            .when('/candidateprofile', {
                templateUrl: '/app/views/candidate/aboutcandidate.html',
                controller: 'candidateProfileController'
            })
            .when('/companylist', {
                templateUrl: '/app/views/company/CompanyList.html',
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
    } else if (role === 'Company') {
        $routeProvider
            .when('/', {
                templateUrl: '/app/views/Company/profile.html',
                controller: 'contractorProfileController'
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

});
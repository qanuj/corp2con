app.config(function ($routeProvider, $locationProvider) {
    var role = document.querySelector('html').dataset.role;
    if (role === 'Contractor') {
        $routeProvider
            .when('/', {
                templateUrl: '/app/views/contractor/profile.html',
                controller: 'contractorController'
            })
            .when('/others', {
                templateUrl: '/app/views/candidate/other.html',
                controller: 'otherController'
            })
            .when('/candidateprofile', {
                templateUrl: '/app/views/candidate/candidateProfile.html',
                controller: 'candidateProfileController'
            })
            .when('/companyprofile', {
                templateUrl: '/app/views/company/companyprofile.html',
                controller: 'companyProfileController'
            })
            .when('/postjobs', {
                templateUrl: '/app/views/job/postjobs.html',
                controller: 'postjobsController'
            })
            .when('/joblisting', {
                templateUrl: '/app/views/job/jobListing.html',
                controller: 'jobListingController'
            })
            .otherwise({ redirectTo:'/' });
    }else if(role === 'Company')
    {
        $routeProvider
           .when('/', {
               templateUrl: '/app/views/company/profile.html',
               controller: 'companyController'
           })
           .otherwise({ redirectTo: '/' });
    }
    $locationProvider.html5Mode(false).hashPrefix('');
});
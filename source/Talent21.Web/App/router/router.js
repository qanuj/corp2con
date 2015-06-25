app.config(function($routeProvider, $locationProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/app/views/home.html',
            controller: 'homeController'
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
        controller: 'companyprofileController'
    })
    .when('/postjobs', {
        templateUrl: '/app/views/job/postjobs.html',
        controller: 'postjobsController'
    })
    .when('/joblisting', {
        templateUrl: '/app/views/job/jobListing.html',
        controller: 'jobListingController'
    })
    .otherwise({
        redirectTo : '/'
    });
    $locationProvider.html5Mode(false).hashPrefix('');
});
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
    .when('/aboutcandidate', {
        templateUrl: '/app/views/candidate/aboutcandidate.html',
        controller: 'aboutCandidateController'
    })

    .otherwise({
        redirectTo : '/'
    });
    $locationProvider.html5Mode(false).hashPrefix('');
});
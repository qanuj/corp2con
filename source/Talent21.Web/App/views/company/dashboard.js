app.controller('companyDashboardController', ['$rootScope', '$scope', 'dataService','$state', function ($rootScope,$scope, db,$state) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = true;
    $rootScope.settings.layout.pageSidebarClosed = false;
    $scope.hideCheckbox= true;

    db.company.dashboard().success(function (result) {
        result.aggregate.duration.min = moment(result.aggregate.duration.min).toDate();
        result.aggregate.duration.max = moment(result.aggregate.duration.max).toDate();
        $scope.record = result;
        
        db.company.search({ location: result.aggregate.location, Skills: result.aggregate.skill, isApplied: false}, 1, 5, 'Availability').success(function (result) {
            $scope.matchingAvailable = result.items;
        });
        db.company.search({ location: result.aggregate.location, Skills: result.aggregate.skill, isApplied: false }, 1, 5, 'Rate').success(function (result) {
            $scope.matchingRate = result.items;
        });
    });

    $scope.nextWeek = moment().add(7, 'days');
    $scope.nextMonth = moment().add(21, 'days');

    db.company.get().success(function (result) {
        $scope.profile = result;
    });

    $scope.search = function (query) {
        $state.go('search', query);
    }

}]);
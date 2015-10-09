app.controller('contractorDashboardController', ['$scope', 'dataService', '$stateParams', '$rootScope','$state', function ($scope, db, $stateParams, $rootScope,$state) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = true;
    $rootScope.settings.layout.pageSidebarClosed = false;

    db.contractor.dashboard().success(function (result) {
        $scope.record = result;
        db.system.getEmployeers({ promotion: 'Advertise', count: 20, locations: result.aggregate.location, skills: result.aggregate.skill }).then(function (result) {
            $scope.topEmployers = result;
        });
        db.contractor.search({ location: result.aggregate.location, skills: result.aggregate.skill }, 1, 5, 'Days').then(function (result) {
            $scope.matching = result.items;
        });
    });
    
    $scope.favorite=function(row){
        if (row.favorite) return;
        db.contractor.favorite(row.id).success(function () {
            row.favorite = new Date();
        });
    }

    $scope.nextWeek = moment().add(7, 'days').format();
    $scope.nextMonth = moment().add(1, 'months').format();

    $scope.search = function (query) {
        $state.go('search',query);
    }


    db.contractor.get().success(function (result) {
        $scope.profile = result;
        if (!result.firstName && !result.lastName) {
            window.location = "#/profile/edit";
        }
    });
}]);


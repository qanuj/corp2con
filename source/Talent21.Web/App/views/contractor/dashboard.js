app.controller('contractorDashboardController', ['$scope', 'dataService', '$routeParams', '$rootScope', function ($scope, db, $routeParams, $rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = true;
    $rootScope.settings.layout.pageSidebarClosed = false;

    db.contractor.dashboard().success(function (result) {
        $scope.record = result;
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

    $scope.search = function (q) {
        window.location = '#/search?q=' + (q.keywords || '') + '&location=' + (q.location || '') + '&skills=' + (q.skills || '');
        return false;
    }

    db.contractor.get().success(function (result) {
        $scope.profile = result;
        $rootScope.profile = result;
        if (!result.firstName && !result.lastName) {
            window.location = "#/profile/edit";
        }
    });
}]);


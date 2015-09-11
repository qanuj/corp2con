app.controller('adminInboxController', ['$scope', 'dataService', '$stateParams','$rootScope','$state', function ($scope, db, $stateParams,$rootScope,$state) {
$scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Inbox";
    $scope.subtitle = $state.current.data.pageTitle;

    $scope.navigate = function (page) {
        db.admin.feedbacks(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.from = (($scope.currentPage-1) * db.pageSize)+1;
            $scope.to = $scope.from + result.items.length - 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.count = result.count;
            $scope.records = result.items;
        });
    }
    
    $scope.navigate($stateParams.page);
}]);


app.controller('adminFeedbackController', ['$scope', 'dataService', '$stateParams','$rootScope', function ($scope, db, $stateParams,$rootScope) {
$scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Site Feedback";

    $scope.navigate = function (page) {
        db.admin.feedbacks(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.count = result.count;
            $scope.records = result.items;
        });
    }
    
    $scope.navigate($stateParams.page);
}]);


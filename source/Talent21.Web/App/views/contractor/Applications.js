app.controller('contractorApplicationsController', ['$scope', 'dataService', '$rootScope', '$stateParams', function ($scope, db,$rootScope, $stateParams) {
        $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Job Applications";
    
    $scope.navigate = function (page) {
        var id = $stateParams.id;
        db.contractor.getJobApplications(id,page).success(function (result) {
                $scope.currentPage = page || 1;
                $scope.pages = Math.ceil(result.count / db.pageSize);
                $scope.records = result.items;
               });              
    }
    $scope.navigate($stateParams.page);
}]);


app.controller('contractorCompanyProfileController', ['$scope', 'dataService', '$stateParams','$rootScope', function ($scope, db, param,$rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Company Profile";

    $scope.role = db.role;
    $scope.page = 1;
    $scope.pages = 1;

    db.job.company(param.id).success(function (result) {
        $scope.record = result;
        $scope.page = db.currentPage;
        $scope.pageUrl = window.location.origin + '/go/company/' + result.companyCode;
        db.job.paged(result.id, $scope.page).success(function (result) {
            $scope.jobs = result.items;
        });
    });


}]);
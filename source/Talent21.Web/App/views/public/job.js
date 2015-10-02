app.controller('publicJobController', ['$scope', 'dataService', '$stateParams', '$rootScope', function ($scope, db, param, $rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Job Profile";
    $scope.role = db.role;
    $scope.id = param.id;

    db.job.byId($scope.id).success(function (job) {
        $scope.record = job;
        $scope.page = window.location.origin + '/go/' + result.job.jobCode;
        db.job.visitJob(param.id);
    });

}]);
app.controller('companyPromoteJobController', ['$scope', 'dataService', '$stateParams', '$rootScope', 'toastr', '$state', function ($scope, db, $stateParams, $rootScope, toastr, $state) {

    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Promote Job";
    $scope.id = $stateParams.id;

    db.job.get($scope.id).success(function (result) {
        $scope.row = result;
    });

    $scope.promotions = [
        { id: 0, title: 'Highlight', credits: 10, color: 'bg-red-sunglo', icon: 'bar-chart-o' },
        { id: 1, title: 'Featured', credits: 20, color: 'bg-green-turquoise', icon: 'paper-plane' },
        { id: 2, title: 'Advertise', credits: 50, color: 'bg-yellow-lemon', icon: 'briefcase' },
        { id: 3, title: 'Global', credits: 100, color: 'bg-blue-hoki', icon: 'globe' }
    ];

    $scope.selectedItem = null;

    $scope.highlight = function (index) {
        $scope.selectedItem = index;
    }

    $scope.showConfirm = function (p) {
        $scope.promotedJob = p;
        $scope.opened = true;
        $scope.promoTitle = p.title;
        $scope.promoCredit = p.credits;
    }

    $scope.confirmPromote = function (jobId) {
        db.company.promoteJob(jobId, $scope.promotedJob.title)
            .success(function (data) {
                toastr.success('Success', 'Job promoted. Enjoy!');
                $state.go('jobs');
            }).error(function (err) {
                toastr.error(err.exceptionMessage, err.message);
            });;
    }
}]);

app.controller('companyPromoteController', ['$scope', 'dataService', '$stateParams', '$rootScope','toastr', function ($scope, db, $stateParams, $rootScope, toastr) {
    
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
        { id: 0, title: 'Highlight', credits: 10 },
        { id: 1, title: 'Featured', credits: 20 },
        { id: 2, title: 'Advertise', credits: 50 },
        { id: 3, title: 'Global', credits: 100 }
    ];

    $scope.promotedJob;

    $scope.showConfirm = function (p) {
        $scope.promotedJob = p;
        $scope.opened = true;
        $scope.promoTitle = p.title;
        $scope.promoCredit = p.credits;
    }

    $scope.confirmPromote = function (jobId) {
        db.company.promoteJob(jobId, $scope.promotedJob.id)
            .success(function (data) {
                toastr.success('Success', 'Job promoted. Enjoy!')
            })
            .error(function (data) {
                console.log(data);
            });
    }
}]);
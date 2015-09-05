app.controller('companyContractorApplicationController', ['$scope', 'dataService', '$stateParams', '$rootScope', function ($scope, db, $stateParams, $rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = true;
    $rootScope.settings.layout.pageSidebarClosed = false;

    function loadSchedule(id) {
        return db.company.getSchedule(id).success(function (result) {
            angular.forEach(result, function (d) {
                d.start = moment(d.start).toDate();
                d.end = moment(d.end).toDate();
            });
            $scope.schedule = result;
        });
    }

    $scope.status = {
        isopen: false
    };

    $scope.hasRejected = true;
    $scope.hasApproved = true;

    $scope.move = function (id, folder) {
        db.company.saveApplication(id, folder).then(navigate);
    }

    $scope.shortlist = function (id) {
        db.company.shortlistApplication(id).then(navigate);
    }

    $scope.reject = function (id) {
        db.company.rejectApplication(id).then(navigate);
    }

    $scope.toggleDropdown = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.isopen = !$scope.status.isopen;
    };

    function loadOthers(jobId, contractorId) {

        db.system.enums('levelEnum').then(function (enums) {
            $scope.levels = enums;
        });

        loadSchedule(contractorId);

        db.contractor.get(contractorId).success(function (result) {
            $scope.record = result;
        });

        db.job.get(jobId).success(function (result) {
            $scope.job = result;
        });
    }

    function navigate() {
        db.company.getJobApplication(param.id).success(function (row) {
            $scope.application = row;
            var act = row.actions[row.actions.length - 1];
            if (act) {
                $scope.hasRejected = act.act !== 'Rejected';
                $scope.hasApproved = act.act !== 'Shortlist';
            }
            loadOthers(row.job.id, row.contractor.id);
        });
    }

    navigate();

}]);
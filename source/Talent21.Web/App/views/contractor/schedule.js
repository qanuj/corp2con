app.controller('contractorScheduleController', ['$scope', 'dataService', '$rootScope', '$stateParams', function ($scope, db,$rootScope, params) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = true;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Manage Schedule";
    $scope.today = new Date();

    $scope.navigate = function (page) {
        db.contractor.pagedSchedule(page).success(function (result) {
            $scope.currentPage = page || 1;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });
    }

    $scope.save = function (record) {
        console.log(record);
        db.contractor.createSchedule({
            start: record.date.startDate.format(),
            end: record.date.endDate.format(),
            company: record.company,
            isAvailable: record.isAvailable
        }).success($scope.navigate).finally(function () {
            $scope.start = '';
            $scope.end = '';
        });
    }

    $scope.update = function (s) {
        db.contractor.editSchedule(s).success($scope.navigate(params.page));
    };

    $scope.delete = function (s) {
        db.contractor.deleteSchedule(s).success($scope.navigate(params.page));
    };

    $scope.clean = function () {
        $scope.record = {};
        $scope.method = 'Add';
    }

    $scope.edit = function (s) {
        s.date = {
            startDate: moment(s.start),
            endDate: moment(s.end)
        }
        $scope.record = s;
        $scope.method = 'Update';
    };

    $scope.navigate(params.page);

    $('#defaultrange').daterangepicker({
        opens: (Metronic.isRTL() ? 'left' : 'right'),
        format: 'MM/DD/YYYY',
        separator: ' to ',
        startDate: moment().subtract('days', 29),
        endDate: moment(),
        minDate: '01/01/2012',
        maxDate: '12/31/2018',
    },
        function (start, end) {
            $('#defaultrange input').val(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        }
    );

    $scope.clean();

}]);
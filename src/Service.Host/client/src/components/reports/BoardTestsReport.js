import React from 'react';
import { Loading, BackButton, ReportTable } from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

const handleBackClick = history => {
    history.push('/production/reports/board-tests-report');
};

const BoardTestsReport = ({ reportData, loading, history }) => (
    <Page>
        <Grid container spacing={3} justify="center">
            <Grid item xs={12}>
                {loading || !reportData ? (
                    <Loading />
                ) : (
                    <ReportTable
                        reportData={reportData}
                        title={reportData.title}
                        showTitle
                        showTotals={false}
                        placeholderRows={3}
                        placeholderColumns={6}
                        showRowTitles={false}
                    />
                )}
            </Grid>
            <Grid item xs={12}>
                <BackButton backClick={() => handleBackClick(history)} />
            </Grid>
        </Grid>
    </Page>
);

BoardTestsReport.propTypes = {
    reportData: PropTypes.shape({}),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    loading: PropTypes.bool,
    options: PropTypes.shape({})
};

BoardTestsReport.defaultProps = {
    reportData: {},
    options: {},
    loading: false
};

export default BoardTestsReport;

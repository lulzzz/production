import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import {
    Loading,
    CreateButton,
    ErrorCard,
    Title,
    PaginatedTable
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

const ViewManufacturingSkills = ({ loading, itemError, history, items }) => {
    const [pageOptions, setPageOptions] = useState({
        orderBy: '',
        orderAscending: false,
        currentPage: 0,
        rowsPerPage: 10
    });
    const [rowsToDisplay, setRowsToDisplay] = useState([]);

    useEffect(() => {
        const compare = (field, orderAscending) => (a, b) => {
            if (!field) {
                return 0;
            }

            if (a[field] < b[field]) {
                return orderAscending ? -1 : 1;
            }

            if (a[field] > b[field]) {
                return orderAscending ? 1 : -1;
            }

            return 0;
        };

        const rows = items.map(el => ({
            skillCode: el.skillCode,
            description: el.description,
            hourlyRate: el.hourlyRate,
            links: el.links
        }));

        if (!rows || rows.length === 0) {
            setRowsToDisplay([]);
        } else {
            setRowsToDisplay(
                rows
                    .sort(compare(pageOptions.orderBy, pageOptions.orderAscending))
                    .slice(
                        pageOptions.currentPage * pageOptions.rowsPerPage,
                        pageOptions.currentPage * pageOptions.rowsPerPage + pageOptions.rowsPerPage
                    )
                    .map(r => ({ ...r, id: r.skillCode }))
            );
        }
    }, [
        pageOptions.currentPage,
        pageOptions.rowsPerPage,
        pageOptions.orderBy,
        pageOptions.orderAscending,
        items
    ]);

    const handleRowLinkClick = href => history.push(href);

    const columns = {
        skillCode: 'Skill Code',
        description: 'Description',
        hourlyRate: 'Hourly Rate'
    };

    return (
        <Page>
            <Title text="Manufacturing Skills" />
            {itemError && <ErrorCard errorMessage={itemError.status} />}
            {loading ? (
                <Loading />
            ) : (
                <Fragment>
                    <Fragment>
                        <CreateButton createUrl="/production/resources/manufacturing-skills/create" />
                    </Fragment>

                    {rowsToDisplay.length > 0 && (
                        <PaginatedTable
                            columns={columns}
                            handleRowLinkClick={handleRowLinkClick}
                            rows={rowsToDisplay}
                            pageOptions={pageOptions}
                            setPageOptions={setPageOptions}
                            totalItemCount={items ? items.length : 0}
                        />
                    )}
                </Fragment>
            )}
        </Page>
    );
};

ViewManufacturingSkills.propTypes = {
    loading: PropTypes.bool.isRequired,
    items: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    itemError: PropTypes.shape({})
};

ViewManufacturingSkills.defaultProps = {
    itemError: null,
    items: []
};

export default ViewManufacturingSkills;

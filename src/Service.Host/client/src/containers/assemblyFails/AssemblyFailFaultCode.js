import { connect } from 'react-redux';
import { getItemErrorDetailMessage } from '@linn-it/linn-form-components-library';
import initialiseOnMount from '../initialiseOnMount';
import AssemblyFailFaultCode from '../../components/assemblyFails/AssemblyFailFaultCode';
import assemblyFailFaultCodeSelectors from '../../selectors/assemblyFailFaultCodeSelectors';
import assemblyFailFaultCodeActions from '../../actions/assemblyFailFaultCodeActions';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: assemblyFailFaultCodeSelectors.getItem(state),
    loading: assemblyFailFaultCodeSelectors.getLoading(state),
    snackbarVisible: assemblyFailFaultCodeSelectors.getSnackbarVisible(state),
    faultCode: match.params.id,
    error: getItemErrorDetailMessage(state, itemTypes.assemblyFailFaultCode.item),
    editStatus: 'create'
});

const initialise = ({ faultCode }) => dispatch => {
    if (faultCode) {
        dispatch(assemblyFailFaultCodeActions.fetch(faultCode));
    }
};

const mapDispatchToProps = {
    initialise,
    addItem: assemblyFailFaultCodeActions.add,
    setSnackbarVisible: assemblyFailFaultCodeActions.setSnackbarVisible,
    setEditStatus: assemblyFailFaultCodeActions.setEditStatus
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(AssemblyFailFaultCode));
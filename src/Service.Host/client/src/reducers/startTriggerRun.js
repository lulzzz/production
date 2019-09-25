﻿import processStoreFactory from './processStoreFactory';
import { startTriggerRunActionTypes as actionTypes } from '../actions';
import * as processTypes from '../processTypes';

const defaultState = { working: false, messageText: '', messageVisible: false };

export default processStoreFactory(
    processTypes.startTriggerRun.actionType,
    actionTypes,
    defaultState,
    'Trigger run start request submitted successfully'
);

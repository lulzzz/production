﻿import { ReportActions } from '@linn-it/linn-form-components-library';
import { whoBuiltWhatReportActionTypes as actionTypes } from './index';
import * as reportTypes from '../reportTypes';
import config from '../config';

export default new ReportActions(
    reportTypes.whoBuiltWhat.item,
    reportTypes.whoBuiltWhat.actionType,
    reportTypes.whoBuiltWhat.uri,
    actionTypes,
    config.appRoot
);

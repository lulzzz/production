﻿import { UpdateApiActions } from '@linn-it/linn-form-components-library';
import { productionTriggerLevel as actionTypes } from './index';
import * as itemTypes from '../itemTypes';
import config from '../config';

export default new UpdateApiActions(
    itemTypes.productionTriggerLevel.item,
    itemTypes.productionTriggerLevel.actionType,
    itemTypes.productionTriggerLevel.uri,
    actionTypes,
    config.appRoot
);

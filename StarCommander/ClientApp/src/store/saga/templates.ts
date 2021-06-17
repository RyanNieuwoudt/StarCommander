import * as R from "ramda";
import { call, put, select } from "redux-saga/effects";
import { Action } from "store";

export const Failure = "_FAILURE";
export const Success = "_SUCCESS";

const getToken = R.path(["auth", "token"]);

function* handleError(error: any, action: Action) {
	const { type, payload } = action;
	yield put({ type: type + Failure, payload, error: error.message });
}

const retry = (generator: any, maxAttempts: number) =>
	function* retry(api: any, action: Action) {
		let attempt = 0;
		while (attempt <= maxAttempts) {
			try {
				yield call(generator, api, action);
				break;
			} catch (error) {
				if (attempt < maxAttempts) {
					//TODO yield log(e);
				} else {
					yield handleError(error, action);
				}
			}
			attempt += 1;
		}
	};

function* command(api: any, action: Action): any {
	const { type, payload } = action;
	const token = yield select(getToken);
	yield call(api, { ...payload, token });
	yield put({ type: type + Success, payload });
}

export const commandSaga = retry(command, 3);

export function* forwardSaga(actionType: string, action: Action) {
	yield put({ ...action, type: actionType });
}

export function* mutateSaga(newValues: any, action: Action) {
	yield put(R.mergeDeepLeft(newValues, action));
}

export function* putSaga(actionCreator: Function) {
	yield put(actionCreator());
}

function* query(api: any, action: Action): any {
	const { type, payload } = action;
	const token = yield select(getToken);
	const data = yield call(api, { ...payload, token });
	yield put({ type: type + Success, payload, data });
}

export const querySaga = retry(query, 3);

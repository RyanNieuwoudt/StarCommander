import * as R from "ramda";
import { connectRouter, routerMiddleware } from "connected-react-router";
import { History } from "history";
import throttle from "lodash.throttle";
import { applyMiddleware, combineReducers, createStore } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
import createSagaMiddleware from "redux-saga";
import { ApplicationState, reducers, sagas } from "store";
import {
	createChannelConnection,
	createSignalRMiddleware,
} from "store/middleware";

const getStateFromSessionStore = () => {
	try {
		const retrievedState = sessionStorage.getItem("reduxStore");
		if (retrievedState === null) {
			return null;
		}
		return JSON.parse(retrievedState);
	} catch (err) {
		return null;
	}
};

export default function configureStore(
	history: History,
	initialState?: ApplicationState
) {
	const isDevelopment = process.env.NODE_ENV === "development";

	const sagaMiddleware = createSagaMiddleware();

	const { connection, connect, disconnect } = createChannelConnection(
		isDevelopment,
		null
	);

	const middleware = [
		sagaMiddleware,
		createSignalRMiddleware(connect, disconnect),
		routerMiddleware(history),
	];

	const rootReducer = combineReducers({
		...reducers,
		router: connectRouter(history),
	});

	if (isDevelopment) {
		const savedState = getStateFromSessionStore();
		if (savedState !== null) {
			initialState = savedState;
		}
	}

	if (connection) {
		connection.on("Message", (message) => {
			store.dispatch(JSON.parse(message));
		});
	}

	const store = createStore(
		rootReducer,
		initialState,
		composeWithDevTools(applyMiddleware(...middleware))
	);

	if (isDevelopment) {
		store.subscribe(
			throttle(() => {
				try {
					sessionStorage["reduxStore"] = JSON.stringify(
						store.getState()
					);
				} catch {
					// ignore write errors
				}
			}, 1000)
		);
	}

	R.forEach((saga) => sagaMiddleware.run(saga), R.values(sagas));

	if (isDevelopment && connect) {
		connect(store.getState().auth.token);
	}

	return store;
}

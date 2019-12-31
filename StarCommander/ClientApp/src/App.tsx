import React, { lazy, Suspense } from "react";
import { Switch } from "react-router";
import { Grommet } from "grommet";
import { grommet } from "grommet/themes";
import { PrivateRoute, PublicRoute } from "components/routing";

const HomePage = lazy(() => import("./routes/HomePage"));
const CounterPage = lazy(() => import("./routes/CounterPage"));
const FetchDataPage = lazy(() => import("./routes/FetchDataPage"));
const SignUpPage = lazy(() => import("./routes/SignUpPage"));

export default () => (
	<Grommet theme={grommet}>
		<Suspense fallback={<div />}>
			<Switch>
				<PublicRoute path="/sign-up" exact component={SignUpPage} />
				<PrivateRoute exact path="/" component={HomePage} />
				<PrivateRoute path="/counter" component={CounterPage} />
				<PrivateRoute
					path="/fetch-data/:startDateIndex?"
					component={FetchDataPage}
				/>
			</Switch>
		</Suspense>
	</Grommet>
);

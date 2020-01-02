import React, { lazy, Suspense } from "react";
import { Switch } from "react-router";
import { Grommet } from "grommet";
import { grommet } from "grommet/themes";
import { PrivateRoute, PublicRoute } from "components/routing";

const HomePage = lazy(() => import("./routes/HomePage"));
const WelcomePage = lazy(() => import("./routes/WelcomePage"));

export default () => (
	<Grommet theme={grommet}>
		<Suspense fallback={<div />}>
			<Switch>
				<PublicRoute path="/welcome" exact component={WelcomePage} />
				<PrivateRoute exact path="/" component={HomePage} />
			</Switch>
		</Suspense>
	</Grommet>
);

import React, { lazy, Suspense } from "react";
import { Switch } from "react-router";
import { Grommet } from "grommet";
import { grommet } from "grommet/themes";
import { PrivateRoute, PublicRoute } from "components/routing";

const Bridge = lazy(() => import("./routes/Bridge"));
const Fleet = lazy(() => import("./routes/Fleet"));
const WelcomePage = lazy(() => import("./routes/WelcomePage"));

export default () => (
	<Grommet theme={grommet}>
		<Suspense fallback={<div />}>
			<Switch>
				<PublicRoute path="/welcome" exact component={WelcomePage} />
				<PrivateRoute exact path="/" component={Bridge} />
				<PrivateRoute exact path="/fleet" component={Fleet} />
			</Switch>
		</Suspense>
	</Grommet>
);

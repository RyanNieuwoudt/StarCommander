import React, { lazy, Suspense } from "react";
import { Route } from "react-router";
import { Grommet } from "grommet";
import { grommet } from "grommet/themes";
import { Layout } from "./components";

const HomePage = lazy(() => import("./routes/HomePage"));
const CounterPage = lazy(() => import("./routes/CounterPage"));
const FetchDataPage = lazy(() => import("./routes/FetchDataPage"));

export default () => (
	<Grommet theme={grommet}>
		<Suspense fallback={<div />}>
			<Layout>
				<Route exact path="/" component={HomePage} />
				<Route path="/counter" component={CounterPage} />
				<Route
					path="/fetch-data/:startDateIndex?"
					component={FetchDataPage}
				/>
			</Layout>
		</Suspense>
	</Grommet>
);

import * as React from "react";
import { Route } from "react-router";
import { Grommet } from "grommet";
import { grommet } from "grommet/themes";
import { Counter, FetchData, Home, Layout } from "./components";

export default () => (
	<Grommet theme={grommet}>
		<Layout>
			<Route exact path="/" component={Home} />
			<Route path="/counter" component={Counter} />
			<Route path="/fetch-data/:startDateIndex?" component={FetchData} />
		</Layout>
	</Grommet>
);

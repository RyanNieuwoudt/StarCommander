import * as React from "react";
import { Box, ResponsiveContext } from "grommet";
import { NavMenu } from ".";

export default function Layout(props: { children?: React.ReactNode }) {
	const size = React.useContext(ResponsiveContext);

	return (
		<>
			<NavMenu />
			<Box pad={size} responsive>
				{props.children}
			</Box>
		</>
	);
}
